using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Modules
{
    public interface IEntityModule
    {
        void Update();
        void Render(Matrix cameraView);
    }
}