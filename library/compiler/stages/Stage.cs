namespace library.compiler.stages
{
    public abstract class Stage<T> : IStage
    {
        private readonly Action<T> postAction;

        protected Stage(Action<T> postAction)
        {
            this.postAction = postAction;
        }

        public void Execute()
        {
            var result = GetResult();
            postAction.Invoke(result);
        }

        public abstract T GetResult();
    }
}