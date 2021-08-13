using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppFB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<FizzBuzzResult> FizzBuzzOutput = new List<FizzBuzzResult>();

        public List<object> inputList = new List<object>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            inputList.Add(1);
            inputList.Add(3);
            inputList.Add(5);
            inputList.Add("");
            inputList.Add("<empty>");  
            inputList.Add(15);
            inputList.Add("A");
            inputList.Add(37);
            inputList.Add(30);
            inputList.Add(45);        
        
        var rules = new List<IRule>
        {
            new FizzMod3Rule(),
            new BuzzMod5Rule(),
            new FizzBuzzNotModRule()    
        };

        FizzBuzzOutput = new FizzBuzzGame(rules).Play(inputList);
            
        }
    }


    public class FizzBuzzGame
{
    private List<IRule> _rules;

    public FizzBuzzGame(List<IRule> rules)
    {
        _rules = rules ?? new List<IRule>();
    }

    public List<FizzBuzzResult> Play(List<object> inputs)
    {

        List<FizzBuzzResult> fizzbuzzResList = new List<FizzBuzzResult>();
        FizzBuzzResult fizzbuzzobj ;


        foreach ( object objinput in inputs )
        {
            var output = string.Empty;
            
            
                if( int.TryParse(objinput.ToString(),out int n))
                {
                    foreach (var rule in _rules) 
                    { 
                      output += rule.Run(n); 
                    }
                }
                else
                {
                    output += new InvalidRule().Run(n);
                }  

                fizzbuzzobj = new FizzBuzzResult(){
                    request = objinput.ToString(),
                    response = output};  

                fizzbuzzResList.Add(fizzbuzzobj); 
        }
        return fizzbuzzResList;
    }
}

public class FizzBuzzResult
{
    public string request {get;set;}
    public string response {get;set;}

}
public interface IRule
{
    string Run(double number);
}

public class FizzMod3Rule : IRule

{
    public string Run(double number)
    {
        return (number % 3) == 0 ? "Fizz" : string.Empty;      
    }
}

public class FizzNotMod3Rule : IRule
{
    public string Run(double number)
    {
        return (number % 3) != 0 ? string.Format("Divide {0} by 3",number) : string.Empty;
    }
}

public class BuzzMod5Rule : IRule
{
    public string Run(double number)
    {
        return (number % 5) == 0 ? "Buzz" : string.Empty;      
    }
}


public class FizzBuzzNotModRule : IRule
{
    public string Run(double number)
    {
        return ((number % 3) != 0 && (number % 5) != 0) ? string.Format("Divide {0} by 3 | Divide {0} by 5",number) : string.Empty;
    }
}


public class InvalidRule : IRule
{
    public string Run(double number)
    {
        return "Invalid Item";
    }
}

}
