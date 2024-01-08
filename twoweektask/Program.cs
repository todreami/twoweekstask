using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace twoweektask
{

    public class Character
    {

        public string Name { get; }

        public string Job { get; }

        public int Level { get; }

        public int Atk { get; }

        public int Def { get; }

        public int Hp { get; }

        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        public int Type { get; }
        public int Atk { get; }

        public int Def { get; }

        public int Hp { get; }

        public bool IsEquipped { get; set; }

        public static int ItemCnt = 0;

        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquipped = isEquipped;
  
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if(withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0}", idx);
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.WriteLine("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));
            Console.Write(" | ");




            //(Atk >- 0 ? "+" : "") [조건 ? 조건이 참이라면 : 조건이 거짓이라면]
            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk}");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def}");
            if (Hp != 0) Console.Write($"Hp {(Hp >= 0 ? "+" : "")}{Hp}");

            Console.Write("|");
            Console.WriteLine(Description);

        }


        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }

    }











    internal class Program
    {
        static Character _player;
        static Item[] _items;

        static void Main(string[] args)
        {
            PrintStartLogo();
            GameDataSetting();
            StartMenu();

        }

        static void StartMenu()
        {

            Console.Clear();
            Console.WriteLine("■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ");
            Console.WriteLine();
            Console.WriteLine("1.상태보기");
            Console.WriteLine("2.인벤토리");
            Console.WriteLine("");

            //유저들이 착할 떄만? 78? 안녕하세요 1번으로 부탁드려요?
            switch (CheckValidInput(1, 2))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
            }

        }

        private static void InventoryMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템목록]");

            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("0, 나가기");
            Console.WriteLine("1.장착관리");
            Console.WriteLine("");
            switch (CheckValidInput(0, 1))
            {
                case 1:
                    StartMenu();
                    break;
                case 2:
                    EquipMenu();
                    break;
            }


        }

        private static void EquipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
           for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i+1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, Item.ItemCnt);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    ToggleEquipStatus(keyInput - 1); //유저가 입력하는건 1, 2, 3 : 실제 배열에는 0, 1, 2...
                    EquipMenu();
                    break;

            }
        }

        private static void ToggleEquipStatus(int idx)
        {
            _items[idx].IsEquipped = !_items[idx].IsEquipped;
        }





            private static void StatusMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 상태 보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithhights("Lv. ", _player.Level.ToString("00"));
            Console.WriteLine("");
            Console.WriteLine("{0} ( {1} )", _player.Name, _player.Job);

            int bonusAtk = getSumBonusAtk();
            PrintTextWithhights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
            int bonusDef = getSumBonusDef();
            PrintTextWithhights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusDef) : "");
            int bonusHp = getSumBonusHp();
            PrintTextWithhights("체력 : ", (_player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");
            PrintTextWithhights("골드 : ", _player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0, 뒤로가기");
            Console.WriteLine("");
            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;

            }
        }

        private static int getSumBonusAtk()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Atk;
            }
            return sum;
        }

        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Def;
            }
            return sum;
        }

        private static int getSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Hp;
            }
            return sum;
        }


        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithhights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }


            private static int CheckValidInput(int min, int max)
        {
            int keyInput; //tryParse
            bool result; //while
            do //일단 한 번 실행을 보장
            {
                Console.WriteLine("원하시는 행동을 입력해주세요");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while (result == false || CheckIfValid(keyInput,min,max) == false);

            //여기에 왔다는 것은 제대로 입력을 받았다는 것
            return keyInput;
        }



        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if(min <= keyInput && keyInput <= max) return true;
            return false;
        }





            private static void PrintStartLogo()
            {

            //Console.WriteLine("o__ __o      o__ __o        o o__ __o ____o__ __o____ o");
            //Console.WriteLine("  / v     vW    <| vW      <|>         <| vW    /   W   /   W    <|>");
            //Console.WriteLine(" />       <W   / W     <W     / W         / W     <W        Wo /         /");
            //Console.WriteLine("_Wo____        Wo / o / o /   Wo       Wo / o /         | o /   Wo");
            //Console.WriteLine("     W_W__o__ | __  _ <|/  <| __ __ |>       | __  _ <|         < >      <| __ __ |>");
            //Console.WriteLine("            W    |          /       W       |       W         |       /       W");
            //Console.WriteLine(" W         /   < o > o /         Wo < o >       Wo o     o /         Wo");
            //Console.WriteLine("o       o |       / v           vW    | vW     <|    / v           vW  ");
            //Console.WriteLine("   <W__ __/>    / W     />             <W  / W         <W    / W  />             <W      ");                                        
            //Console.WriteLine("o__ __o     __o__ o          o o         o o                   o ____o__ __o____ o__ __o o__ __o");
            //Console.WriteLine("   / v     vW      |    <|W        /|>  <|>       <|>  <|>                 <|>    /   W   /   W     / v     vW      <| vW      ");
            //Console.WriteLine(" />       <W    / W   / WWo o// W  / W       / W  / W                 / W         Wo/         />       <W     / W     <W     ");
            //Console.WriteLine("_Wo____         Wo /   Wo / vW  / v Wo /  Wo /       Wo /  Wo / o /   Wo | o /           Wo   Wo / o /W");
            //Console.WriteLine("     W_W__o__ |     |   <W/>   |    |         |    |               <| __ __ |>      < >      <|             |>   | __  _ <");
            //Console.WriteLine("           W    < >   / W        / W  < >       < >  / W              /       W       |        WW           //    |       W     ");
            //Console.WriteLine(" W         /     |    Wo /        Wo /   W         /   Wo / o /         Wo o          W         /     < o >       Wo");
            // Console.WriteLine("o       o o     |          | o       o |            / v           vW   <| o       o | vW ");
            // Console.WriteLine("  <W__ __/> __ |> _ / W        / W    <W__ __/>    / W _Wo__ / _ />             <W  / W          <W__ __/>      / W         <W");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("                          PRESS ANYKEY TO START                                      ");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine();

            }











            private static void GameDataSetting()
            {
               _player = new Character("chad", "전사", 1, 10, 5, 100, 1500);
               _items = new Item[10];
               AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0));
               AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0));
            }




            static void AddItem(Item item)
            {
               if (Item.ItemCnt == 10) return;
               _items[Item.ItemCnt] = item; // 0개 -> 0번 인덱스 / 1개 -> 1번 인덱스
               Item.ItemCnt++;
            }



        }
}
