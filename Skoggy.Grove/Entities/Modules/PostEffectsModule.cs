using Microsoft.Xna.Framework;
using Skoggy.Grove.Rendering.PostFx;

namespace Skoggy.Grove.Entities.Modules
{
    public class PostEffectsModule : IEntityModule
    {
        public EntityWorld EntityWorld { get; set; }
        private PostFxPipeline _pipeline;

        public PostFxPipeline Pipeline => _pipeline;

        public PostEffectsModule()
        {
            _pipeline = new PostFxPipeline();
        }

        public void Update()
        {
        }

        public void PreRender(Matrix view)
        {
            Pipeline.Begin();
        }

        public void Render(Matrix cameraView)
        {
            Pipeline.End();
        }
    }
}