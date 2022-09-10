using System.Text;

namespace Gradebook.Foundation.Common.Extensions;

public static class StringExtensions
{
    public static string GetRandom(this string str, int length){
        StringBuilder str_build = new StringBuilder();  
        Random random = new Random();  
        char letter;  
        for (int i = 0; i < length; i++)
        {
        double flt = random.NextDouble();
        int shift = Convert.ToInt32(Math.Floor(25 * flt));
        letter = Convert.ToChar(shift + 65);
        str_build.Append(letter);  
        }  
        return str_build.ToString();
    }
}
