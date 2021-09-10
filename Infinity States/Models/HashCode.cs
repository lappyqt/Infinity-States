using System.Text;

namespace Infinity_States.Models 
{
    public static class HashCode
    {
        public static string GenerateHashCode(string text)
        {
            string result = "";
            byte[] byteArray = Encoding.Default.GetBytes(text);

            for (int i = 0; i < byteArray.Length; i++) 
                result += byteArray[i];

            return result;
        }
    }
}