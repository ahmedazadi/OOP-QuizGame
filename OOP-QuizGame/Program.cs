using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            ManageUser m1 = new ManageUser();
            User theUser;
            Subject theSubject;
            while (true)
            {
                int o = Choice.Option(new string[] { "login", "register", "show all" }, "Main Menu");
                if (o == 0)
                {
                    while (true)
                    {
                        theUser = m1.Login();
                        if (theUser == null)
                        { break; }
                        else
                        {
                            while (true)
                            {
                                theSubject = theUser.ShowSubjects();
                                if (theSubject.name != "exit")
                                {
                                    theSubject.QuestMenu();
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                if (o == 1)
                {
                    Console.Clear();
                    m1.AddUser();
                }
                if (o == 2)
                {
                    Console.Clear();
                    m1.Show();
                    Console.ReadKey();
                }
            }
        }
    }


    class ManageUser
    {
        public User[] user; //data member
        public ManageUser() //constructure
        {
            user = new User[2];
            user[0] = new User(1, "ahmed", "000");
            user[1] = new User(2, "hajar", "2001");
        }
        public void AddUser() //add a new user
        {
            Console.Clear();
            Color.Yellow("Register");
            Color.Gray("in order to create a new account you have to");

            //Console.Write("enter username: ");
            Color.Lable("enter your username: ");
            string name = Console.ReadLine();

            Console.Write("enter your password: ");
            string password = Console.ReadLine();

            User[] user2 = new User[user.Length + 1];

            for (int i = 0; i < user.Length; i++)
            {
                user2[i] = user[i];
            }
            user2[user.Length] = new User((user.Length + 1), name, password);
            user = user2;
        }
        public User Login() //this method is called when user logs in
        {
            Console.Clear();  //header
            Color.Yellow("log in");   //header
            Color.Gray("type \"exit\" to go back"); //header

            while (true)
            {
                Color.Lable("Username: ");
                string username = Console.ReadLine();
                if (username == "exit")
                    return null;

                Color.Lable("Password: ");
                string password = Color.Password();
                if (password == "exit")
                    return null;

                for (int i = 0; i < user.Length; i++)
                {
                    if (username == user[i].name && password == user[i].password)
                    {
                        return user[i];
                    }
                    if (i == user.Length)
                    {
                        break;
                    }
                }
                Color.Alert("password is wrong, please try again");
            }
        }
        public void Show() //show all the user names and passwords
        {
            Color.Gray("press any keys to exit");
            Color.Yellow("recorded users:\n");

            for (int i = 0; i < user.Length; i++)
            {
                Console.WriteLine("ID: {0}\nusername: {1}\npassword: {2}", user[i].id, user[i].name, user[i].password);
                Console.WriteLine("------------------");
            }
        }
    }


    class Person //this class is only used for inheritance
    {
        public int id;
        public string name;
    }


    class User : Person
    {
        public string password;
        public Subject[] subject;
        public User(int id, string name, string password)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.subject = new Subject[3];
            this.subject[0] = new Subject("Programming");
            this.subject[1] = new Subject("Web Design");
            this.subject[2] = new Subject("Network");
        }
        public Subject ShowSubjects()   //this method is for showing all the Subjects that a user has available
        {
            string[] tempName = new string[subject.Length + 1];

            for (int i = 0; i < subject.Length; i++)     //this loop saves names of all the subjects in a string array
            {
                tempName[i] = subject[i].name;
            }
            tempName[subject.Length] = "log out";

            int correctSubject = Choice.Option(tempName, "your subjects are below");

            if (tempName[correctSubject] == "log out")
            {
                return new Subject("exit");
            }
            else
            {
                return subject[correctSubject];
            }
        }
        public void NewSubject()
        {
            string subjectName = InputFilter.NoEmptyString();

            Subject[] tempSub = new Subject[subject.Length + 1];
            for (int i = 0; i < subject.Length; i++)
            {
                tempSub[i].name = subject[i].name;
            }
            tempSub[subject.Length] = new Subject(subjectName);
        } //add a new subject
    }


    class Subject : Person
    {
        public Question[] question;

        /// default question
        private string que = "What Programming is used for building IOS apps natively?";
        private string[] ans = new string[3] { "Javascript", "HTML", "Swift" };
        int correct = 3;
        /// default question

        public Subject(string name) //constructure
        {
            this.name = name;
            question = new Question[1];
            question[0] = new Question(que, ans, correct);
        }
        private void AddQuestion() //add a new question to the list
        {
            Console.Clear();
            GUI.StillHeader("Add a Question to " + this.name, "You have to fill the form by typing");
            Color.Lable("enter your Question");
            string question = Console.ReadLine();

            Color.Lable("Enter three options\n");

            string[] options = InputFilter.UserOptionInput(3);

            Color.Lable("The numebr of rihgt answer");
            int rightAns = InputFilter.IntBetween(1, 3);

            Question[] tempQ = new Question[this.question.Length + 1];
            for (int i = 0; i < this.question.Length; i++)
            {
                tempQ[i] = this.question[i];
            }

            tempQ[this.question.Length] = new Question(question, options, rightAns);
            this.question = tempQ;

        }
        private void Test()
        {
            int score = 0;
            GUI.StillHeader("test", "choose the correct answer by entering the numbers.\npress any key to exit");

            for (int i = 0; i < question.Length; i++)
            {
                Color.Lable("Q" + (i + 1) + "/ " + question[i].question);
                Console.WriteLine();
                for (int k = 0; k < question[i].answer.Length; k++)
                {
                    Console.WriteLine((k + 1) + ". " + question[i].answer[k]);
                }
                int ans = InputFilter.IntBetween(1, 3);
                if (ans == question[i].rightAnswer)
                {
                    score++;
                    Color.RightAns("your answer is right");
                    Console.WriteLine("press any key to move to the next quesiton...");
                    Console.ReadKey();
                }
                else
                {
                    Color.Alert("WRONG, study better next time");
                }
                Color.Gray("^^^^^^^^^^^^^^^^^^^");
            }

            Console.ReadKey();
        }
        private void ShowQ()  //print all the questions and answers on the terminal
        {
            GUI.StillHeader("All the Qhestions and Answers");

            Console.WriteLine("Questions:");
            for (int i = 0; i < question.Length; i++)
            {

                Color.Lable("Quesion");
                Console.WriteLine(question[i].question);
                for (int k = 0; k < question[i].answer.Length; k++)
                {
                    Color.Lable("answer Number " + k + ":");
                    Console.WriteLine(question[i].answer[k]);
                }
                Color.Lable("++++++++++++++++++++++++");
            }
            Console.ReadLine();
        }
        public void QuestMenu()
        {
            string[] m = { "take the test", "see all the questions and answers", "add your own Questions", "exit" };
            int inp = Choice.Option(m, "Quesiton menue");
            if (inp == 0)
            {
                Test();
            }
            if (inp == 1)
            {
                ShowQ();
            }
            if (inp == 2)
            {
                AddQuestion();
            }

        }
    }


    class Question
    {
        public string question;
        public string[] answer;
        public int rightAnswer;
        public Question(string question, string[] answer, int rightAnswer)
        {
            this.question = question;
            this.answer = answer;
            this.rightAnswer = rightAnswer;
        }
    }


    class Choice
    {
        public static int Option(string[] option, string heading)
        {
            int n = 0;
            while (true)
            {
                GUI.header(heading);

                //function itself
                for (int i = 0; i < option.Length; i++)
                {
                    if (i == n)
                        Color.GrayBack((i + 1) + ". " + option[i] + "  ");

                    else
                        Console.WriteLine((i + 1) + ". " + option[i] + "  ");
                }
                ConsoleKeyInfo k = Console.ReadKey();
                if (k.Key == ConsoleKey.Enter)
                {
                    return n;
                }
                if (k.Key == ConsoleKey.UpArrow && n > 0)
                {
                    n--;
                }
                if (k.Key == ConsoleKey.DownArrow && n < option.Length - 1)
                {
                    n++;
                }
            }
        }
    }


    class Color
    {
        private static void Default()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Gray(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(text);
            Default();
        }
        public static void Gray2(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Default();
        }
        public static void Yellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Default();
        }
        public static void GrayBack(string text)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(text);
            Default();
        }
        public static void Lable(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Default();
            Console.Write(' ');
        }
        public static void Alert(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Default();
        }
        public static void RightAns(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Default();
        }
        public static string Password()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            string g = Console.ReadLine();
            Default();
            return g;
        }
    }


    class GUI
    {
        public static void header(string heading)
        {
            Console.Clear();
            Color.Yellow(heading);

            Color.Gray2("on your keyborad use ");
            Console.Write("Arrow");
            Color.Gray2(" and ");
            Console.Write("Enter");
            Color.Gray(" keys.");
        }
        public static void StillHeader(string heading)
        {
            Console.Clear();
            Color.Yellow(heading);
        }
        public static void StillHeader(string heading, string guide)
        {
            Console.Clear();
            Color.Yellow(heading);
            Color.Gray(guide);
        }
    }


    class InputFilter
    {
        public static string[] UserOptionInput(int numberOfArray)//this method reads an array of strings forom user
        {
            string[] value = new string[numberOfArray];

            for (int i = 0; i < value.Length; i++)
            {
                Console.WriteLine("enter options number" + (i + 1));
                value[i] = Console.ReadLine();
            }
            return value;
        }
        public static string NoEmptyString()
        {
            string input;
            int count = 0;
            do
            {
                if (count > 0)
                {
                    Color.Alert("you can't leave it empty");
                }
                input = Console.ReadLine();
            }
            while (input == "");
            return input;
        }
        public static int IntBetween(int a, int b)
        {
            int n;
            int count = 0;
            do
            {
                if (count > 0)
                    Console.WriteLine("number unaccepted\ntry again");

                n = Convert.ToInt32(Console.ReadLine());
                count++;
            }
            while (n < a || n > b);
            return n;
        }

    }
