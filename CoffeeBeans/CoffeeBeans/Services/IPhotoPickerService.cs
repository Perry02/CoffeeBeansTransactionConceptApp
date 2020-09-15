using System.IO;
using System.Threading.Tasks;

namespace CoffeeBeans
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
