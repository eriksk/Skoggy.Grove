using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Modules
{
    public interface IEntityModule
    {
        EntityWorld EntityWorld { get; set; }
        void Update();
        void Render(Matrix cameraView);
        void PreRender(Matrix view);
    }
}