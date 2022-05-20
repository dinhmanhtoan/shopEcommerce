using System.Security.Cryptography;
using System.Text;

namespace Model.ViewModel.NganLuong;

public  class SecurityHelper
{
    public static string MD5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "").ToLower();
        }
    }
}

