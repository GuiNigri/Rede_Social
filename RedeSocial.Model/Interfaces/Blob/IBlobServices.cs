using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Model.Interfaces.Blob
{
    public interface IBlobServices
    {
        Task<string> CreateBlobAsync(string imageBase64);
        Task DeleteBlobAsync(string blobName);
    }
}
