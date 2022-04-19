using System.ComponentModel.DataAnnotations;

namespace Cowsandbulls.Models
{
    public class Solver : IValidatableObject
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(24,MinimumLength =4)]
        
        public string query { get; set; } = null!;

        //Validation works only if format is 4356,2345,2895
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var outresult = new List<ValidationResult>();
            
            if (checkiflenthfour(query) && checkifvalueisinteger(query) && onlycomma(query))
            {
                if (uniquedigit(query))
                { }
                else
                {
                    outresult.Add(new ValidationResult("Every four digit value must have unique digits", new[] { "query" }));
                }              
            }
            else
            {
               outresult.Add(new ValidationResult("One or more value is not of four digits OR four digit values must be seperated by comma OR one of the digit is not integer ", new[] { "query" }));
            }
            
            return outresult;

        }

        //To chek whether every four digit number has unique digits.
        private bool uniquedigit(string query)
        {
            var qry = query.ToString().Split(",").ToList();
            foreach(var item in qry)
            {
                for(int i = 0; i < item.Length; i++)
                {
                    //check whether a digit occurs more than once in a four digit value.
                    if (morethan(item[i], stringwithoutcharat_i(item, i)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        //returns a value without a digit at position i
        private string stringwithoutcharat_i(string item, int i)
        {
            if(i == 0)
            {
                return (item.Substring(1, item.Length - 1));
            }
            else
            {
                return (item.Substring(0, i) + item.Substring(i + 1, (item.Length - (i + 1))));
            }
        }

        //check whether a digit(D) occurs more than once in four digit value whithout that specified digit(D). 
        private bool morethan(char v, string stringwithout_v)
        { 
            if(stringwithout_v.Contains(v))
            {
                return true;
            }
            return false;
        }

        //Check whether a query submitted having four digit values must be seperated by comma.
        private bool onlycomma(string query)
        {    
            for(int i = 4; i < query.Length; i=i+5)
            {
                if(query[i] == ',')
                {               
                }
                else
                {
                  return false;
                }
            }
            return true;

        }

        //Check whether every value entered is an integer 
        private bool checkifvalueisinteger(string query)
        {
            var qry = query.ToString().Split(",").ToList();
            foreach (var item in qry)
            {
                foreach (var s in item)
                {
                    if (!char.IsDigit(s))
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        //To check every value must be of length four.
        private static bool checkiflenthfour(string query)
        {
            var qry = query.ToString().Split(",").ToList();
            foreach (var item in qry)
            {
                if (item.Length != 4)
                {
                    return false;
                }
            }
            return true; 
        }

    }
}

