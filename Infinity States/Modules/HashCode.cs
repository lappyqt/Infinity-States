using System.Text;

namespace Infinity_States.Modules 
{
    public struct HashCode
    {
        /// <summary>
        ///  Generate Hash Code, using default byte encoding.
        /// </summary>
        public string GenerateHashCode(string text)
        {
            string result = string.Empty;
            byte[] byteArray = Encoding.Default.GetBytes(text);

            for (int i = 0; i < byteArray.Length; i++) 
                result += byteArray[i];

            return result;
        }
    }
}