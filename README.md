# Advent of Code

* Advent of Code is an Advent calendar of small programming puzzles for a variety of skill sets and skill levels that can be solved in any programming language you like. People use them as a speed contest, interview prep, company training, university coursework, practice problems, or to challenge each other.

* You don't need a computer science background to participate - just a little programming knowledge and some problem solving skills will get you pretty far. Nor do you need a fancy computer; every problem has a solution that completes in at most 15 seconds on ten-year-old hardware.

* LockDown Fun Just for a bit of lockdown fun thought, I would dust of my old C# skills.

* Probably not the best or most efficient code, might go back and refine later, (or try in Rust, haskell, Dart or Python...) but gets all the right answers...

* Lots of skills to practice (well, I used all these....) File and IO functions String manipulation single, 2d and 3d arrays Binary data manipulation Recursice functions (lots of...) Regex functions Vectors Hastables Lists, queues, stacks, search algorithms and dictionaries, Images (as arrays) Data searching and analysing Encryption

# To use this framework
### Framework
* IDE
    * Visual Studio 2019
* Folder Structure
    * Top level - All the framework files
    * Year Index - Markdown details of all solutions for given year
    * Year - Solutions for given year
    * Year/ Data - Input data for given year (format Day*Input.txt)
    * Year/ Answers - Answers for given year (format Day*Output.txt)
* Solutions Structure
    * Solutions have 2 interfaces Day1 and Day2
    * Namespace is defined per year and then class name
    * All solutions inherit from baseline, this contains
        * Definitions for framework atributes
        * Project extensions (e.g. Strings, Arrays)
        * Project reusable functions
    * Input data is automitically seleced if file is present based in Namespace and Class name
   
# Solution example
```
namespace AdventOfCode.Y2017
{
    [ProblemName("Day06: ...")]
    class Day06 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input).First();
        
        private IEnumerable<object> Day1(string inData)
        {
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {inData}";
        }
    }
}
```
### Adding years
* Create the following Folders
    * Year e.g. "2017"
    * In year "Answers" and "Data" folders
    * Create solutions in the year folder from solution template (above)

### Running Solutions
* In the project properties -> Debug -> Application aguments 
    * To run all solutions - Leave blank
    * To run a specific year - e.g. "year=2017"
    * To run a specific solution - e.g. "year=2017 day=Day01"