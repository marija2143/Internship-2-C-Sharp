using System;

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
        //napravit fju za ...
        static string Check_SubMenu()
        {
            bool incorrectInput = true;
            string first_choice = "";

            while (incorrectInput)
            {
                Console.WriteLine("Unesite broj: ");
                var _choice = Console.ReadLine();
                int b;
                var check_num = int.TryParse(_choice, out b); 
                try
                {
                    if (check_num ||_choice.Length>1)
                    {
                        var ex1 = new Exception("Neispravan unos, nije slovo");
                        throw ex1;
                    }
                    //if (first_choice < 1 || first_choice > biggest_option)
                    //{
                    //    var ex2 = new Exception("Neispravan unos, odabir van ponuđenih");
                    //    throw ex2;
                    //}
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return first_choice;
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
                    if (choice_user % 2 == 0)
                    {
                        Console.WriteLine(sub_options_menu_user[choice_user - 2]);

                        //check sub menu
                    }
                    //posalji izbor u funkciju koja ce izvrsavat odabrano (switch)
                    break;

                case 2:
                    Console.WriteLine(options_menu1[1]);
                    var choice_acc = Check_Menu(acc_options_num);
                    var sub_choice_acc = "";
                    if (choice_acc!=3)
                    {
                        Console.WriteLine(sub_options_menu_account[choice_acc - 1]);
                        //check sub menu
                    }
                    //posalji izbor u funkciju koja ce izvrsavat odabrano (switch)
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
