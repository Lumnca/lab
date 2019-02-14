using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabExam.IServices
{
    public interface IEncryptionDataService
    {
        String EncodeByRsa(String Data);

        String DecryptByRsa(String Data);

        String EncodeByMd5(String Data);

        String EncodeByMd5Times(String Data, int Time);
    }
}
