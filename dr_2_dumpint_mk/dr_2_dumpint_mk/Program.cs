using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        static void User_Function_1()
        {
            //Add user
            
            bool incorrectInput = true;
            var new_User="";
            while (incorrectInput)
            {
                Console.WriteLine("Unesite ime: ");
                var name = Console.ReadLine();
                Console.WriteLine("Unesite prezime: ");
                var l_name = Console.ReadLine();
                Console.WriteLine("Unesite datum rodenja (dd/mm/gggg): ");
                var date = Console.ReadLine();
                var date_ = new DateTime();
                var d = DateTime.TryParse(date,out date_);
                try
                {
                    if (name=="" || l_name=="" || d==false || name.Length<2 || l_name.Length<2 )
                    {
                        var ex1 = new Exception("Provjerite unos podataka. Ime i prezime moraju imati barem dva znaka, a datum mora biti unesen u tocnom formatu.");
                        throw ex1;
                    }

                    List<int> indices = new List<int>();
                    using (StreamReader sr = new StreamReader("userbase.txt"))
                    {
                        var line = sr.ReadLine();
                        while (line != null)
                        {
                            //Console.WriteLine("crta " + line);
                            var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                            string info = "";
                            foreach (var item in line_)
                            {
                                //Console.WriteLine("item: " + item);
                                if (item != '[' || item != ']' || item != ' ' || item != ';')
                                {
                                    info += item;
                                }
                                else continue;
                            }
                           // Console.WriteLine("fml2 " + info);
                            var finfo_ = info.Split(",");
                            //Console.WriteLine("fml3 / index :" + finfo_[0]);
                            indices.Add(int.Parse(finfo_[0]));
                            line = sr.ReadLine();
                        }
                    }
                    
                    var biggest_index = indices[indices.Count-1]+1;
                    new_User = $"[{biggest_index},{name},{l_name},{date_}];";
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            using (StreamWriter sw=new StreamWriter("userbase.txt",true))
            {
                if(new_User!="")
                {
                    sw.WriteLine(new_User);
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        } 
        //done
        static void User_Function_2(string option_)
        {
            //Delete user: a) by id, b) by first and last name
            var dict = new Dictionary<int, string>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");

                    dict.Add(int.Parse(finfo_[0]), info);
                    line = sr.ReadLine();
                }
            }
            switch (option_)
            {
                case "a":
                    bool incorrectInput = true;
                    int id_ = 0;

                    while (incorrectInput)
                    {
                        Console.WriteLine("Unesite id korisnika kojeg zelite izbrisati: ");
                        var _choice = Console.ReadLine();
                        var f_choice = int.TryParse(_choice, out id_);
                        try
                        {
                            if (!f_choice)
                            {
                                var ex1 = new Exception("Neispravan unos, nije broj");
                                throw ex1;
                            }
                            if (dict.ContainsKey(id_)==false)
                            {
                                var ex2 = new Exception("Neispravan unos, id ne postoji");
                                throw ex2;
                            }
                            incorrectInput = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    incorrectInput = true;
                    while (incorrectInput)
                    {
                        Console.WriteLine("Jeste li sigurni da zelite izbrisati korinika " + id_.ToString() + " ? (Y/N) ");
                        var choice_ = Console.ReadLine();
                        if (choice_.ToUpper()=="Y")
                        {
                            dict.Remove(id_);
                            using (StreamWriter sw = new StreamWriter("userbase.txt"))
                            {
                                foreach (var item in dict)
                                {
                                    var str_info = "["+item.Value.ToString()+"];";
                                    sw.WriteLine(str_info);
                                }
                            }
                            Console.WriteLine("Korisnik izbrisan.");
                            incorrectInput = false;
                        }
                        if (choice_.ToUpper()=="N")
                        {
                            break;
                        }
                        else { Console.WriteLine("Unesite Y za da ili N za ne."); }
                    }
                    break;
                case "b":
                    incorrectInput = true;
                    var name_lname = "";

                    while (incorrectInput)
                    {
                        Console.WriteLine("Unesite ime i prezime korisnika kojeg zelite izbrisati: ");
                        var _choice = Console.ReadLine();
                        try
                        {
                            if (_choice.Trim()=="")
                            {
                                var ex1 = new Exception("Neispravan unos, niste unijeli ime");
                                throw ex1;
                            }
                            incorrectInput = false;
                            name_lname = _choice;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    var name = name_lname.Split()[0];
                    var lname = name_lname.Split()[1];
                    incorrectInput = true;
                    foreach (var user in dict)
                    {
                        var user_ = user.Value;
                        var key_ = user.Key;
                        var arr = user_.Split(',');
                        Console.WriteLine("username " + arr[1] + arr[2]);
                        if (arr[1].ToLower()==name.ToLower() && arr[2].ToLower()==lname.ToLower())
                        {
                            while (incorrectInput)
                            {
                                Console.WriteLine("Jeste li sigurni da zelite izbrisati korinika " + name_lname+ " ? (Y/N) ");
                                var choice_ = Console.ReadLine();
                                if (choice_.ToUpper() == "Y")
                                {
                                    dict.Remove(key_);
                                    using (StreamWriter sw = new StreamWriter("userbase.txt"))
                                    {
                                        foreach (var item in dict)
                                        {
                                            var str_info = "[" + item.Value.ToString() + "];";
                                            sw.WriteLine(str_info);
                                        }
                                    }
                                    Console.WriteLine("Korisnik izbrisan.");
                                    incorrectInput = false;
                                }
                                if (choice_.ToUpper() == "N")
                                {
                                    break;
                                }
                                else { Console.WriteLine("Unesite Y za da ili N za ne."); }
                            }
                        }
                    }
                    break;
                default: Console.WriteLine("Nesto je poslo po krivu"); break;
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //done
        static void User_Function_3()
        {
            //Update user by id
            var dict = new Dictionary<int, string>();
            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    Console.WriteLine(info);
                    dict.Add(int.Parse(finfo_[0]), info);
                    line = sr.ReadLine();
                }
            }
            bool incorrectInput = true;
            var id_ = 0;
            while (incorrectInput)
            {
                Console.WriteLine("Unesite id korisnika kojeg zelite promijeniti: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            incorrectInput = true;
            var new_User = "";
            while (incorrectInput)
            {
                Console.WriteLine("Unesite ime: ");
                var name = Console.ReadLine();
                Console.WriteLine("Unesite prezime: ");
                var l_name = Console.ReadLine();
                Console.WriteLine("Unesite datum rodenja (dd/mm/gggg): ");
                var date = Console.ReadLine();
                var date_ = new DateTime();
                var d = DateTime.TryParse(date, out date_);
                try
                {
                    if (name == "" || l_name == "" || d == false || name.Length < 2 || l_name.Length < 2)
                    {
                        var ex1 = new Exception("Provjerite unos podataka. Ime i prezime moraju imati barem dva znaka, a datum mora biti unesen u tocnom formatu.");
                        throw ex1;
                    }

                    new_User = $"{id_},{name},{l_name},{date_}";
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            if (new_User!="")
            {
                dict[id_] = new_User;
            }
            using (StreamWriter sw = new StreamWriter("userbase.txt"))
            {
                foreach (var item in dict)
                {
                    var str_info = "[" + item.Value.ToString() + "];";
                    sw.WriteLine(str_info);
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //done
        static void User_Function_4(string option_)
        {
            //Show users: a) aplhabetically by last name , b) over 30 y.o. , c) in debt

            var dict = new Dictionary<int, string>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");

                    dict.Add(int.Parse(finfo_[0]), info);
                    line = sr.ReadLine();
                }
            }

            switch (option_)
            {
                case "a":
                    var sort_dict = new Dictionary<string, string>();
                    foreach (var item in dict)
                    {
                        var key = item.Value.Split(",")[2];
                        if (sort_dict.ContainsKey(key))
                        {
                            sort_dict.Add(key + "a", item.Value);
                        }
                        else sort_dict.Add(key, item.Value);
                    }

                    var sorted_dict = new SortedDictionary<string, string>();
                    foreach (var item in sort_dict)
                    {
                        if (sorted_dict.ContainsKey(item.Key))
                        {
                            sorted_dict.Add(item.Key.ToLower()+"a", item.Value);
                        }
                        else sorted_dict.Add(item.Key.ToLower(), item.Value);
                    }

                    foreach (var item in sorted_dict)
                    {
                        var split = item.Value.Split(",");
                        Console.WriteLine($"{split[0]} - {split[1]} - {split[2]} - {split[3]}");
                    }
                    break;
                case "b":
                    foreach (var item in dict)
                    {
                        var split = item.Value.Split(",");
                        TimeSpan diff = DateTime.Today.Date - DateTime.Parse(split[3]).Date;
                        if ((diff.TotalDays/365)>30)
                        {
                            Console.WriteLine(split[1]+" "+split[2]);
                        }
                    }
                    break;
                case "c":
                    Console.WriteLine("Samo test");
                    break;
                default:
                    break;
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //2 of 3 done
        static void Account_Function_1(string option_)
        {
            //Insert transaction: a) current time , b) past time
            var dict = new Dictionary<int, List<string>>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    var list = new List<string>();
                    dict.Add(int.Parse(finfo_[0]), list);
                    Console.WriteLine("postoji "+finfo_[0]);
                    line = sr.ReadLine();
                }
            }
            using (StreamReader sr = new StreamReader("transactionbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split("\\");
                    var user_id = int.Parse(finfo_[0]);
                    Console.WriteLine(user_id);
                    dict[user_id].Add(info);
                    line = sr.ReadLine();
                }
            }
            bool id_input = true;
            var id_ = 0;
            while (id_input)
            {
                Console.WriteLine("Unesite id korisnika ciji racun zelite vidjeti: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    id_input = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            bool incorrectInput = true;
            var new_Trsc = "";
            DateTime date_ = new DateTime();
            int id_transaction=0;
            if (dict[id_].Count>0)
            {
                var last_trans_id = dict[id_][dict[id_].Count - 1].Split("\\")[1];
                id_transaction = int.Parse(last_trans_id) + 1;
            }
            else id_transaction = 1;
            
            while (incorrectInput)
            {
                Console.WriteLine("Unesite opis: ");
                var description = Console.ReadLine();
                if (description.Trim()=="")
                {
                    description = "Standardna transakcija";
                }
                double amount=0;
                while (amount<=0)
                {
                    Console.WriteLine("Unesite iznos (mora biti veci od 0 i biti napisan kao xy,ab): ");
                    var temp = double.TryParse(Console.ReadLine(), out amount);
                }
                Console.WriteLine("Unesite tip: \n \t a) prihod" +
                    "\n \t b) rashod ");
                string[] sub1 = { "a", "b" };
                var t_type = Check_SubMenu(sub1);
                var t_type_ = "";

                string[] sub2 = { "a", "b", "c" };
                var category = "";
                var category_ = "";

                if (t_type == "a")
                {
                    t_type_ = "prihod";
                    Console.WriteLine("Odaberite kategoriju: \n \t a) placa" +
                        "\n \t b) honorar" +
                        "\n \t c) poklon");
                    category = Check_SubMenu(sub2);
                    switch (category)
                    {
                        case "a":
                            category_ = "placa";
                            break;
                        case "b":
                            category_ = "honorar";
                            break;
                        case "c":
                            category_ = "poklon";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    t_type_ = "rashod";
                    Console.WriteLine("Odaberite kategoriju: \n \t a) hrana" +
                        "\n \t b) prijevoz" +
                        "\n \t c) sport");
                    category = Check_SubMenu(sub2);
                    switch (category)
                    {
                        case "a":
                            category_ = "hrana";
                            break;
                        case "b":
                            category_ = "prijevoz";
                            break;
                        case "c":
                            category_ = "sport";
                            break;
                        default:
                            break;
                    }

                }
                Console.WriteLine("tip: " + t_type_ + " des: " + description + " cat: " + category_ + " amo: " + amount);
                if (t_type_ != "" && description != "" && category_ != "")
                {
                    new_Trsc = $"{id_}\\{id_transaction}\\{amount}\\{description}\\{t_type_}\\{category_}";
                    incorrectInput = false;
                }
                switch (option_)
                {
                    case "a":
                        date_ = DateTime.Now;
                        new_Trsc = "[" + new_Trsc + "\\"+date_+"];";
                        break;
                    case "b":
                        incorrectInput = true;
                        while (incorrectInput)
                        {
                            Console.WriteLine("Unesite datum provodenja (dd/mm/gggg): ");
                            var date = Console.ReadLine();
                            var d = DateTime.TryParse(date, out date_);
                            if (d == false)
                            {
                                incorrectInput = true;
                            }
                            else incorrectInput = false;
                        }
                        new_Trsc = "[" + new_Trsc + "\\" + date_ + "];";
                        break;
                    default:
                        break;
                }
            }
            dict[id_].Add(new_Trsc);
            using (StreamWriter sw = new StreamWriter("transactionbase.txt",true))
            {
                sw.WriteLine(new_Trsc);
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //done
        static void Account_Function_2(string option_)
        {
            //Delete transaction: a) by id , b) under amount , c) over amount , d) all revenues ,
            //e) all expenses , f) all transactions in category
            var dict = new Dictionary<int, List<string>>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    var list = new List<string>();
                    dict.Add(int.Parse(finfo_[0]), list);
                    Console.WriteLine("postoji " + finfo_[0]);
                    line = sr.ReadLine();
                }
            }
            using (StreamReader sr = new StreamReader("transactionbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split("\\");
                    var user_id = int.Parse(finfo_[0]);
                    Console.WriteLine(user_id);
                    dict[user_id].Add(info);
                    line = sr.ReadLine();
                }
            }

            bool id_input = true;
            var id_ = 0;
            while (id_input)
            {
                Console.WriteLine("Unesite id korisnika ciji racun zelite pogledati: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    id_input = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            switch (option_)
            {
                case "a":
                    bool incorrectInput = true;
                    var id_t = 0;
                    var id_t_ = new List<int>();
                    int index_in_list;
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[1];
                        id_t_.Add(int.Parse(split));
                    }
                    while (incorrectInput)
                    {
                        Console.WriteLine("Unesite id transakcije koju zelite izbrisati: ");
                        var _choice = Console.ReadLine();
                        var f_choice = int.TryParse(_choice, out id_t);
                        try
                        {
                            if (!f_choice)
                            {
                                var ex1 = new Exception("Neispravan unos, nije broj");
                                throw ex1;
                            }
                            if (id_t_.Contains(id_t) == false)
                            {
                                var ex2 = new Exception("Neispravan unos, id ne postoji");
                                throw ex2;
                            }
                            incorrectInput = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    index_in_list = id_t_.IndexOf(id_t);
                    dict[id_].RemoveAt(index_in_list);
                    break;

                case "b":
                    incorrectInput = true;
                    float amnt = 0;
                    var amounts_ = new List<float>();
                    var indices_in_list= new List<int>();
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[2];
                        amounts_.Add(float.Parse(split));
                    }
                    while (incorrectInput)
                    {
                        Console.WriteLine("Unesite iznos ispod kojeg zelite izbrisati: ");
                        var _choice = Console.ReadLine();
                        var f_choice = float.TryParse(_choice, out amnt);
                        try
                        {
                            if (!f_choice)
                            {
                                var ex1 = new Exception("Neispravan unos, nije broj");
                                throw ex1;
                            }
                            foreach (var item in amounts_)
                            {
                                if (item < amnt)
                                {
                                    int ind = amounts_.IndexOf(item);
                                    indices_in_list.Add(ind);
                                }
                                else continue;
                            }
                            incorrectInput = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    foreach (var item in indices_in_list)
                    {
                        dict[id_].RemoveAt(item);
                    }
                    break;

                case "c":
                    incorrectInput = true;
                    amnt = 0;
                    amounts_ = new List<float>();
                    indices_in_list = new List<int>();
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[2];
                        amounts_.Add(float.Parse(split));
                    }
                    while (incorrectInput)
                    {
                        Console.WriteLine("Unesite iznos iznad kojeg zelite izbrisati: ");
                        var _choice = Console.ReadLine();
                        var f_choice = float.TryParse(_choice, out amnt);
                        try
                        {
                            if (!f_choice)
                            {
                                var ex1 = new Exception("Neispravan unos, nije broj");
                                throw ex1;
                            }
                            foreach (var item in amounts_)
                            {
                                if (item > amnt)
                                {
                                    int ind = amounts_.IndexOf(item);
                                    Console.WriteLine("ind ", ind);
                                    indices_in_list.Add(ind);
                                }
                                else continue;
                            }
                            incorrectInput = false;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    foreach (var item in indices_in_list)
                    {
                        dict[id_].RemoveAt(item);
                    }
                    break;

                case "d":
                    string rev = "prihod";
                    var types_ = new List<string>();
                    indices_in_list = new List<int>();
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[4];
                        types_.Add(split);
                    }
                    foreach (var item in types_)
                    {
                        if (item == rev)
                        {
                            int ind = types_.IndexOf(item);
                            indices_in_list.Add(ind);
                        }
                        else continue;
                    }
                    foreach (var item in indices_in_list)
                    {
                        dict[id_].RemoveAt(item);
                    }
                    break;
                case "e":
                    rev = "rashod";
                    types_ = new List<string>();
                    indices_in_list = new List<int>();
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[4];
                        types_.Add(split);
                    }
                    foreach (var item in types_)
                    {
                        if (item == rev)
                        {
                            int ind = types_.IndexOf(item);
                            indices_in_list.Add(ind);
                        }
                        else continue;
                    }
                    foreach (var item in indices_in_list)
                    {
                        dict[id_].RemoveAt(item);
                    }
                    break;
                case "f":
                    Console.WriteLine("Odaberite kategoriju: \n\t a) placa" +
                        "\n\t b) honorar" +
                        "\n\t c) poklon" +
                        "\n\t d) hrana" +
                        "\n\t e) prijevoz" +
                        "\n\t f) sport");
                    string[] sub_list = {"a","b","c","d","e","f"};
                    string categ_ = Check_SubMenu(sub_list);
                    var categ="";
                    switch (categ_)
                    {
                        case "a":
                            categ = "placa";
                            break;
                        case "b":
                            categ = "honorar";
                            break;
                        case "c":
                            categ = "poklon";
                            break;
                        case "d":
                            categ = "hrana";
                            break;
                        case "e":
                            categ = "prijevoz";
                            break;
                        case "f":
                            categ = "sport";
                            break;
                        default:
                            break;
                    }
                    var cats_= new List<string>();
                    indices_in_list = new List<int>();
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\")[5];
                        cats_.Add(split);
                    }
                    foreach (var item in cats_)
                    {
                        if (item == categ)
                        {
                            int ind = cats_.IndexOf(item);
                            indices_in_list.Add(ind);
                        }
                        else continue;
                    }
                    foreach (var item in indices_in_list)
                    {
                        dict[id_].RemoveAt(item);
                    }
                    break;
                default:
                    break;
            }

            using (StreamWriter sw = new StreamWriter("transactionbase.txt"))
            {
                foreach (var item in dict)
                {
                    foreach (var list_item in item.Value)
                    {
                        var write = "[" + list_item + "];";
                        sw.WriteLine(write);
                    }
                }
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //done
        static void Account_Function_3()
        {
            //Edit transaction by id
            var dict = new Dictionary<int, List<string>>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    var list = new List<string>();
                    dict.Add(int.Parse(finfo_[0]), list);
                    Console.WriteLine("postoji " + finfo_[0]);
                    line = sr.ReadLine();
                }
            }
            using (StreamReader sr = new StreamReader("transactionbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split("\\");
                    var user_id = int.Parse(finfo_[0]);
                    Console.WriteLine(user_id);
                    dict[user_id].Add(info);
                    line = sr.ReadLine();
                }
            }

            bool id_input = true;
            var id_ = 0;
            while (id_input)
            {
                Console.WriteLine("Unesite id korisnika ciji racun zelite pogledati: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    id_input = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            bool incorrectInput = true;
            var id_t = 0;
            var id_t_ = new List<int>();
            int index_in_list;
            foreach (var item in dict[id_])
            {
                var split = item.Split("\\")[1];
                id_t_.Add(int.Parse(split));
            }
            while (incorrectInput)
            {
                Console.WriteLine("Unesite id transakcije koju zelite promijeniti: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_t);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (id_t_.Contains(id_t) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    incorrectInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            index_in_list = id_t_.IndexOf(id_t);
            incorrectInput = true;
            var new_Trsc = "";
            DateTime date_ = new DateTime();
            while (incorrectInput)
            {
                Console.WriteLine("Unesite opis: ");
                var description = Console.ReadLine();
                if (description.Trim() == "")
                {
                    description = "Standardna transakcija";
                }
                double amount = 0;
                while (amount <= 0)
                {
                    Console.WriteLine("Unesite iznos (mora biti veci od 0 i biti napisan kao xy,ab): ");
                    var temp = double.TryParse(Console.ReadLine(), out amount);
                }
                Console.WriteLine("Unesite tip: \n \t a) prihod" +
                    "\n \t b) rashod ");
                string[] sub1 = { "a", "b" };
                var t_type = Check_SubMenu(sub1);
                var t_type_ = "";

                string[] sub2 = { "a", "b", "c" };
                var category = "";
                var category_ = "";

                if (t_type == "a")
                {
                    t_type_ = "prihod";
                    Console.WriteLine("Odaberite kategoriju: \n \t a) placa" +
                        "\n \t b) honorar" +
                        "\n \t c) poklon");
                    category = Check_SubMenu(sub2);
                    switch (category)
                    {
                        case "a":
                            category_ = "placa";
                            break;
                        case "b":
                            category_ = "honorar";
                            break;
                        case "c":
                            category_ = "poklon";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    t_type_ = "rashod";
                    Console.WriteLine("Odaberite kategoriju: \n \t a) hrana" +
                        "\n \t b) prijevoz" +
                        "\n \t c) sport");
                    category = Check_SubMenu(sub2);
                    switch (category)
                    {
                        case "a":
                            category_ = "hrana";
                            break;
                        case "b":
                            category_ = "prijevoz";
                            break;
                        case "c":
                            category_ = "sport";
                            break;
                        default:
                            break;
                    }

                }
                Console.WriteLine("tip: " + t_type_ + " des: " + description + " cat: " + category_ + " amo: " + amount);
                if (t_type_ != "" && description != "" && category_ != "")
                {
                    new_Trsc = $"{id_}\\{id_t}\\{amount}\\{description}\\{t_type_}\\{category_}";
                    incorrectInput = false;
                }

                Console.WriteLine("Unesite datum provodenja (dd/mm/gggg): ");
                var date = Console.ReadLine();
                var d = DateTime.TryParse(date, out date_);
                if (d == false)
                {
                    incorrectInput = true;
                }
                else incorrectInput = false;
                new_Trsc = new_Trsc + "\\" + date_;
            }
            dict[id_][index_in_list] =new_Trsc;

            using (StreamWriter sw = new StreamWriter("transactionbase.txt"))
            {
                foreach (var item in dict)
                {
                    foreach (var list_item in item.Value)
                    {
                        var write = "[" + list_item + "];";
                        sw.WriteLine(write);
                    }
                }
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }
        //done
        static void Account_Function_4(string option_)
        {
            //View transactions: a) as occured ,  b) sorted upward by amount , c) sorted downward by amount ,
            //d) sorted alphabetically by desctiption , e) sorted upward by date , f) sorted downward by date ,
            //g) all revenues , h) all expenses , i) for given category (all)
            var dict = new Dictionary<int, List<string>>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    var list = new List<string>();
                    dict.Add(int.Parse(finfo_[0]), list);
                    Console.WriteLine("postoji " + finfo_[0]);
                    line = sr.ReadLine();
                }
            }
            using (StreamReader sr = new StreamReader("transactionbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split("\\");
                    var user_id = int.Parse(finfo_[0]);
                    Console.WriteLine(user_id);
                    dict[user_id].Add(info);
                    line = sr.ReadLine();
                }
            }

            bool id_input = true;
            var id_ = 0;
            while (id_input)
            {
                Console.WriteLine("Unesite id korisnika ciji racun zelite pogledati: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    id_input = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            switch (option_)
            {
                case "a":
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\");
                        Console.WriteLine(split[4]+ " - "+split[2]+" - "+split[3]+" - ", split[5]+" - ");
                        Console.WriteLine(split[6]);
                    }
                    break;

                case "b":
                    var sort_dict = new Dictionary<float, List<string>>();
                    foreach (var item in dict[id_])
                    {
                        var key = float.Parse(item.Split("\\")[2]);
                        if (sort_dict.ContainsKey(key))
                        {
                            sort_dict[key].Add(item);
                        }
                        else
                        {
                            var new_list = new List<string>();
                            new_list.Add(item);
                            sort_dict.Add(key, new_list);
                        }
                    }
                    foreach (var item in sort_dict.OrderBy(x => x.Key))
                    {
                        if (item.Value.Count>1)
                        {
                            foreach (var transact in item.Value)
                            {
                                var split = transact.Split("\\");
                                Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                                Console.WriteLine(split[6]);
                            }
                        }
                        else
                        {
                            var split = item.Value[0].Split("\\");
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "c":

                    sort_dict = new Dictionary<float, List<string>>();
                    foreach (var item in dict[id_])
                    {
                        var key = float.Parse(item.Split("\\")[2]);
                        if (sort_dict.ContainsKey(key))
                        {
                            sort_dict[key].Add(item);
                        }
                        else
                        {
                            var new_list = new List<string>();
                            new_list.Add(item);
                            sort_dict.Add(key, new_list);
                        }
                    }
                    foreach (var item in sort_dict.OrderByDescending(x => x.Key))
                    {
                        if (item.Value.Count > 1)
                        {
                            foreach (var transact in item.Value)
                            {
                                var split = transact.Split("\\");
                                Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                                Console.WriteLine(split[6]);
                            }
                        }
                        else
                        {
                            var split = item.Value[0].Split("\\");
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "d":

                    var sort_dict_ = new Dictionary<string, List<string>>();
                    foreach (var item in dict[id_])
                    {
                        var key = item.Split("\\")[3];
                        if (sort_dict_.ContainsKey(key))
                        {
                            sort_dict_[key].Add(item);
                        }
                        else
                        {
                            var new_list = new List<string>();
                            new_list.Add(item);
                            sort_dict_.Add(key, new_list);
                        }
                    }
                    foreach (var item in sort_dict_.OrderBy(x => x.Key))
                    {
                        if (item.Value.Count > 1)
                        {
                            foreach (var transact in item.Value)
                            {
                                var split = transact.Split("\\");
                                Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                                Console.WriteLine(split[6]);
                            }
                        }
                        else
                        {
                            var split = item.Value[0].Split("\\");
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "e":
                    sort_dict_ = new Dictionary<string, List<string>>();
                    foreach (var item in dict[id_])
                    {
                        var key = item.Split("\\")[6];
                        if (sort_dict_.ContainsKey(key))
                        {
                            sort_dict_[key].Add(item);
                        }
                        else
                        {
                            var new_list = new List<string>();
                            new_list.Add(item);
                            sort_dict_.Add(key, new_list);
                        }
                    }
                    foreach (var item in sort_dict_.OrderBy(x => x.Key))
                    {
                        if (item.Value.Count > 1)
                        {
                            foreach (var transact in item.Value)
                            {
                                var split = transact.Split("\\");
                                Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                                Console.WriteLine(split[6]);
                            }
                        }
                        else
                        {
                            var split = item.Value[0].Split("\\");
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "f":
                    sort_dict_ = new Dictionary<string, List<string>>();
                    foreach (var item in dict[id_])
                    {
                        var key = item.Split("\\")[6];
                        if (sort_dict_.ContainsKey(key))
                        {
                            sort_dict_[key].Add(item);
                        }
                        else
                        {
                            var new_list = new List<string>();
                            new_list.Add(item);
                            sort_dict_.Add(key, new_list);
                        }
                    }
                    foreach (var item in sort_dict_.OrderByDescending(x => x.Key))
                    {
                        if (item.Value.Count > 1)
                        {
                            foreach (var transact in item.Value)
                            {
                                var split = transact.Split("\\");
                                Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                                Console.WriteLine(split[6]);
                            }
                        }
                        else
                        {
                            var split = item.Value[0].Split("\\");
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "g":
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\");
                        if (split[4]=="prihod")
                        {
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "h":
                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\");
                        if (split[4] == "rashod")
                        {
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                case "i":
                    Console.WriteLine("Odaberite kategoriju: \n\t a) placa" +
                        "\n\t b) honorar" +
                        "\n\t c) poklon" +
                        "\n\t d) hrana" +
                        "\n\t e) prijevoz" +
                        "\n\t f) sport");
                    string[] sub_list = { "a", "b", "c", "d", "e", "f" };
                    string categ_ = Check_SubMenu(sub_list);
                    var category = "";
                    switch (categ_)
                    {
                        case "a":
                            category = "placa";
                            break;
                        case "b":
                            category = "honorar";
                            break;
                        case "c":
                            category = "poklon";
                            break;
                        case "d":
                            category = "hrana";
                            break;
                        case "e":
                            category = "prijevoz";
                            break;
                        case "f":
                            category = "sport";
                            break;
                        default:
                            break;
                    }

                    foreach (var item in dict[id_])
                    {
                        var split = item.Split("\\");
                        if (split[5]==category)
                        {
                            Console.WriteLine(split[4] + " - " + split[2] + " - " + split[3] + " - ", split[5] + " - ");
                            Console.WriteLine(split[6]);
                        }
                    }
                    break;

                default:
                    break;
            }
            using (StreamWriter sw = new StreamWriter("transactionbase.txt"))
            {
                foreach (var item in dict)
                {
                    foreach (var list_item in item.Value)
                    {
                        var write = "[" + list_item + "];";
                        sw.WriteLine(write);
                    }
                }
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();

        }
        //done
        static void Account_Function_5(string option_)
        {
            //Financial report: a) current account status , b) number of total transactions ,
            //c) total for revenur+es and expenses by month and year , d) percentage of expenses for category ,
            //e) avg transaction amount for month and year , f) avg transaction amount for category
            var dict = new Dictionary<int, List<string>>();

            using (StreamReader sr = new StreamReader("userbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split(",");
                    var list = new List<string>();
                    dict.Add(int.Parse(finfo_[0]), list);
                    Console.WriteLine("postoji " + finfo_[0]);
                    line = sr.ReadLine();
                }
            }
            using (StreamReader sr = new StreamReader("transactionbase.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var line_ = line.Trim(new char[] { ']', '[', ';', '\n' });
                    string info = "";
                    foreach (var item in line_)
                    {
                        if (item != '[' || item != ']' || item != ' ' || item != ';')
                        {
                            info += item;
                        }
                        else continue;
                    }
                    var finfo_ = info.Split("\\");
                    var user_id = int.Parse(finfo_[0]);
                    Console.WriteLine(user_id);
                    dict[user_id].Add(info);
                    line = sr.ReadLine();
                }
            }

            bool id_input = true;
            var id_ = 0;
            while (id_input)
            {
                Console.WriteLine("Unesite id korisnika ciji racun zelite pogledati: ");
                var _choice = Console.ReadLine();
                var f_choice = int.TryParse(_choice, out id_);
                try
                {
                    if (!f_choice)
                    {
                        var ex1 = new Exception("Neispravan unos, nije broj");
                        throw ex1;
                    }
                    if (dict.ContainsKey(id_) == false)
                    {
                        var ex2 = new Exception("Neispravan unos, id ne postoji");
                        throw ex2;
                    }
                    id_input = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            using (StreamWriter sw = new StreamWriter("transactionbase.txt"))
            {
                foreach (var item in dict)
                {
                    foreach (var list_item in item.Value)
                    {
                        var write = "[" + list_item + "];";
                        sw.WriteLine(write);
                    }
                }
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastavak.");
            Console.ReadKey(); //buffer
            Start();
        }


        static void Start() {
            Console.Clear();
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
                    "\n 4 - Pregled transakcija"+
                    "\n 5 - Financijsko izvjesce"
            };

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
                    "\n \t i) sve transakcije za odabranu kategoriju",
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
                    var choice_user = Check_Menu(user_options_num);
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
                    if (choice_acc == 2 || choice_acc==1)
                    {
                        Console.WriteLine(sub_options_menu_account[choice_acc - 1]);
                    }
                    else { Console.WriteLine(sub_options_menu_account[choice_acc - 2]); }
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
                            string[] sub_opt4_acc = { "a", "b", "c", "d", "e", "f", "g", "h", "i" };
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

                case 3:
                    ;
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
            static void Main(string[] args)
        {
            Start();
        }
    }
}
