using System;
using System.IO;
namespace dr_2_dumpint_mk
{
    class Program
    {
        static int Check_Menu(int biggest_option)
        {
            bool incorrectInput = true;
            int first_choice = 0;

            while (incorrectInput)
            {
                Console.WriteLine("Unesite broj: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out first_choice);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (first_choice < 1 || first_choice > biggest_option)
                    {
                        var ex2 = new Exception("Neispravan unos, broj van ponuđenih");
                        throw ex2;
                    }
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return first_choice;
        }
        static string Check_SubMenu(string[] letter_opt)
        {
            bool incorrectInput = true;
            string first_choice = "";

            while (incorrectInput)
            {
                Console.WriteLine("Unesite slovo: ");
                var _choice = Console.ReadLine();
                int b;
                var check_num = int.TryParse(_choice, out b); 
                try
                {
                    if (check_num ||_choice.Length>1)
                    {
                        var ex1 = new Exception("Neispravan unos, nije jedno slovo");
                        throw ex1;
                    }

                    bool inArray = false;
                    for (int i = 0; i < letter_opt.Length; i++)
                    {
                        if (letter_opt[i].ToLower()==_choice.ToLower())
                        {
                            inArray = true;
                        }
                    }
                    if (inArray!=true)
                    {
                        var ex1 = new Exception("Neispravan unos, nije ponudeno slovo");
                        throw ex1;
                    }

                    first_choice = _choice;
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return first_choice;
        }

        static string User_Function_1()
        {
            //Add user
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string User_Function_2(string option_)
        {
            //Delete user: a) by id, b) by first and last name
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string User_Function_3()
        {
            //Update user by id
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string User_Function_4(string option_)
        {
            //Show users: a) aplhabetically by last name , b) over 30 y.o. , c) in debt
            StreamReader sr = new StreamReader("");
            return "";
        }

        static string Account_Function_1(string option_)
        {
            //Insert transaction: a) current time , b) past time
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string Account_Function_2(string option_)
        {
            //Delete transaction: a) by id , b) under amount , c) over amount , d) all revenues ,
            //e) all expenses , f) all transactions in category
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string Account_Function_3()
        {
            //Edit transaction by id
            StreamReader sr = new StreamReader("");
            StreamWriter sw = new StreamWriter("");
            return "";
        }
        static string Account_Function_4(string option_)
        {
            //View transactions: a) as occured ,  b) sorted upward by amount , c) sorted downward by amount ,
            //d) sorted alphabetically by desctiption , e) sorted upward by date , f) sorted downward by date ,
            //g) all revenues , h) all expenses , i) for given category (all) , j) for given type and category (all)
            StreamReader sr = new StreamReader("");
            return "";
        }
        static string Account_Function_5(string option_)
        {
            //Financial report: a) current account status , b) number of total transactions ,
            //c) total for revenur+es and expenses by month and year , d) percentage of expenses for category ,
            //e) avg transaction amount for month and year , f) avg transaction amount for category
            StreamReader sr = new StreamReader("");
            return "";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(" 1 - Korisnici \n 2 - Računi \n 3 - Izlaz iz aplikacije ");

            var first_options_num = 3;
            var user_options_num = 4;
            var acc_options_num = 5;

            string[] options_menu1 = { " 1 - Unos novog korisnika" +
                    "\n 2 - Brisanje korisnika" +
                    "\n 3 - Uređivanje korisnika (id)" +
                    "\n 4 - Pregled korisnika", 
                " 1 - Unos nove transakcije" +
                    "\n 2 - Brisanje transakcije" +
                    "\n 3 - Uređivanje transakcije (id)" +
                    "\n 4 - Pregled transakcija"};

            string[] sub_options_menu_user = {
                    " 2 - Brisanje korisnika: " +
                    "\n \t a) po id-u" +
                    "\n \t b) po imenu i prezimenu",
                " 4 - Pregled korisnika" +
                    "\n \t a) abecedni ispis po prezimenu" +
                    "\n \t b) ispis korisnika starijih od 30" +
                    "\n \t c) ispis korisnika u minusu"};

            string[] sub_options_menu_account = {" 1 - Unos nove transakcije: " +
                    "\n \t a) trenutno izvrsena transakcija" +
                    "\n \t b) ranije izvrsena transakcija",
                    " 2 - Brisanje transakcija: " +
                    "\n \t a) po id-u" +
                    "\n \t b) ispod unesenog iznosa" +
                    "\n \t c) iznad unesenog iznosa" +
                    "\n \t d) svih prihoda" +
                    "\n \t e) svih rashoda" +
                    "\n \t f) svih transakcija za odabranu kategoriju",
                " 4 - Pregled transakcija" +
                    "\n \t a) klasicno" +
                    "\n \t b) sortirano po iznosu (uzlazno)" +
                    "\n \t c) sortirano po iznosu (silazno)" +
                    "\n \t d) sortirano po opisu (abecedno)"+
                    "\n \t e) sortirano po datumu (uzlazno)"+
                    "\n \t f) sortirano po datumu (silazno)"+
                    "\n \t g) svi prihodi"+
                    "\n \t h) svi rashodi"+
                    "\n \t i) sve transakcije za odabranu kategoriju"+
                    "\n \t j) sve transakcije za odabrani tip i kategoriju",
                " 5 - Financijsko izvjesce:" +
                "\n \t a) trenutno stanje racuna" +
                "\n \t b) broj ukupnih transakcija" +
                "\n \t c) ukupan iznos prihoda i rashoda za odabrani mjesec i godinu" +
                "\n \t d) postotak udjela rashoda za odabranu kategoriju" +
                "\n \t e) prosjecni iznos transakcije za odabrani mjesec i godinu" +
                "\n \t f) prosjecni iznos transakcije za odabranu kategoriju"
            };

            switch (Check_Menu(first_options_num))
            {
                case 1:
                    Console.WriteLine(options_menu1[0]);
                    var choice_user=Check_Menu(user_options_num);
                    var sub_choice_user = "";
                    if (choice_user == 2)
                    {
                        Console.WriteLine(sub_options_menu_user[0]);
                    }
                    else if (choice_user == 4)
                    {
                        Console.WriteLine(sub_options_menu_user[1]);
                    }
                    switch (choice_user)
                    {
                        case 1:
                            User_Function_1();
                            break;

                        case 2:
                            string[] sub_opt2_user = { "a", "b" };
                            sub_choice_user = Check_SubMenu(sub_opt2_user);

                            User_Function_2(sub_choice_user);
                            break;

                        case 3:
                            User_Function_3();
                            break;

                        case 4:
                            string[] sub_opt4_user = { "a", "b", "c" };
                            sub_choice_user = Check_SubMenu(sub_opt4_user);

                            User_Function_4(sub_choice_user);
                            break;

                        default: Console.WriteLine("Nesto je poslo po krivu"); break;
                    }
                    break;

                case 2:
                    Console.WriteLine(options_menu1[1]);
                    var choice_acc = Check_Menu(acc_options_num);
                    var sub_choice_acc = "";
                    if (choice_acc!=3)
                    {
                        Console.WriteLine(sub_options_menu_account[choice_acc - 1]);
                    }
                    switch (choice_acc)
                    {
                        case 1:
                            string[] sub_opt1_acc = { "a", "b" };
                            sub_choice_acc = Check_SubMenu(sub_opt1_acc);

                            Account_Function_1(sub_choice_acc);
                            break;

                        case 2:
                            string[] sub_opt2_acc = { "a", "b", "c", "d", "e", "f" };
                            sub_choice_acc = Check_SubMenu(sub_opt2_acc);

                            Account_Function_2(sub_choice_acc);
                            break;

                        case 3:
                            Account_Function_3();
                            break;

                        case 4:
                            string[] sub_opt4_acc = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
                            sub_choice_acc = Check_SubMenu(sub_opt4_acc);

                            Account_Function_4(sub_choice_acc);
                            break;

                        case 5:
                            string[] sub_opt5_acc = { "a", "b", "c", "d", "e", "f" };
                            sub_choice_acc = Check_SubMenu(sub_opt5_acc);

                            Account_Function_5(sub_choice_acc);
                            break;

                        default: Console.WriteLine("Nesto je poslo po krivu"); break;
                    }
                    break;

                case 3:;
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Dovidenja! Pritisnite bilo koju tipku za izlaz. \n");
                    Environment.Exit(0);
                    break;

                default: Console.WriteLine("Nesto je poslo po krivu"); break;
            }

            Console.ReadKey(); //buffer
        }
    }
}
