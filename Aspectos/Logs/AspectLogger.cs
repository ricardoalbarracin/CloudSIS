using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Web;

namespace BLComponents
{
    internal class AspectLogger : IMessageSink
    {
        internal AspectLogger(IMessageSink next)
        {
            m_next = next;
        }

        #region Private Vars
        private IMessageSink m_next;
        private String m_typeAndName;
        private string target;
        private IMethodMessage call;
        #endregion // Private Vars

        #region IMessageSink implementation
        public IMessageSink NextSink
        {
            get { return m_next; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Preprocess(msg);
            IMessage returnMethod = m_next.SyncProcessMessage(msg);
            PostProcess(msg, returnMethod);
            return returnMethod;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new InvalidOperationException();
        }
        #endregion //IMessageSink implementation

        #region Helper methods
        private void Preprocess(IMessage msg)
        {
            // We only want to process method calls
            if (!(msg is IMethodMessage)) return;

            call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            m_typeAndName = type.Name + "." + call.MethodName;
            Debug.Write("PreProcessing: " + m_typeAndName + "(");
            target = Type.GetType(call.TypeName).FullName + "." + call.MethodName;
            //LogWrapper.Trace("Llamado a capa de negocio", target, call.Args);

            // Loop through the [in] parameters
            for (int i = 0; i < call.ArgCount; ++i)
            {
                if (i > 0) Debug.WriteLine(", ");
                Debug.Write(call.GetArgName(i) + " = " + call.GetArg(i));
            }
            Debug.WriteLine(")");
        }

        private void PostProcess(IMessage msg, IMessage msgReturn)
        {
            // We only want to process method return calls
            if (!(msg is IMethodMessage) ||
                !(msgReturn is IMethodReturnMessage)) return;

            IMethodReturnMessage retMsg = (IMethodReturnMessage)msgReturn;
            Debug.Write("PostProcessing: ");
            Exception ex = retMsg.Exception;
            if (ex != null)
            {
                Debug.WriteLine("Exception was thrown: " + ex);

                ///LogWrapper.Error(ex, target, call.Args);
                //HttpContext.Current.Items["isInspectorBlCatched"] = true;
                return;
            }
            // Loop through all the [out] parameters
            Debug.Write(m_typeAndName + "(");
            if (retMsg.OutArgCount > 0)
            {
                Debug.Write("out parameters[");
                for (int i = 0; i < retMsg.OutArgCount; ++i)
                {
                    if (i > 0) Debug.Write(", ");
                    Debug.Write(retMsg.GetOutArgName(i) + " = " + retMsg.GetOutArg(i));
                }
                Debug.Write("]");
            }
            if (retMsg.ReturnValue.GetType() != typeof(void))
                Debug.Write(" returned [" + retMsg.ReturnValue + "]");

            Debug.WriteLine(")\n");
        }
        #endregion Helpers
    }

    public class InterceptProperty : IContextProperty, IContributeObjectSink
    {
        #region IContributeObjectSink implementation
        public IMessageSink GetObjectSink(MarshalByRefObject o, IMessageSink next)
        {
            return new AspectLogger(next);
        }
        #endregion // IContributeObjectSink implementation

        #region IContextProperty implementation
        public string Name
        {
            get
            {
                return "CallAspectLoggerProperty";
            }
        }
        public void Freeze(Context newContext)
        {
        }
        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }
        #endregion //IContextProperty implementation
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class AspectLoggerAttribute : ContextAttribute
    {
        public AspectLoggerAttribute()
            : base("AspectLogger")
        {
        }

        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new InterceptProperty());
        }
    }

}