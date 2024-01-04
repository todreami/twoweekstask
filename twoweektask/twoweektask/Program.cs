namespace twoweektask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            Console.Write("\n1.상태보기\n2.인벤토리\n3.상점");


            startScene();



            //List<int> numbers = new List<int>();
            //numbers.Add(1);
            //numbers.Add(2);
            //numbers.Add(3);

            //foreach (int number in numbers)
            //{
            //    Console.WriteLine(number);
            //}

            //-----------------------------------------------------------------------


            // 캐릭터 정보

        }

        public void startScene()
        {
            while (true)
            {
                Console.Write("\n원하시는 행동을 입력해주세요.\n >>");
                int input = int.Parse(Console.ReadLine());

                if (input == 1)
                {
                    Console.WriteLine("1.상태보기");

                    int[] playerStats = new int[5];

                    // 캐릭터의 능력치를 확인      
                    for (int i = 0; i < playerStats.Length; i++)
                    {

                    }

                    // 상태창정보
                    Console.WriteLine("Lv. " + playerStats[0]);
                    Console.WriteLine("Chad ( 전사 ) ");
                    Console.WriteLine("공격력 : " + playerStats[1]);
                    Console.WriteLine("방어력 : " + playerStats[2]);
                    Console.WriteLine("체  력 : " + playerStats[3]);
                    Console.WriteLine("Gold : " + playerStats[4] + " G");

                    break;

                }
                if (input == 2)
                {
                    Console.WriteLine("2.인벤토리");

                    int[] inventory = new int[6];

                    // 인벤토리 관리

                    for (int i = 0; i < inventory.Length; i++)
                    {

                    }

                    // 상태창정보
                    Console.WriteLine("장착 관리\n " + inventory[0]);


                    break;
                }
                if (input == 3)
                {

                    int[] shop = new int[5];

                    for (int i = 0; i < shop.Length; i++)
                    {

                    }


                    //=====================================


                    // 아이템 목록
                    Console.WriteLine("[아이템 목록]");
                    Console.WriteLine("수련자갑옷 | 방어력 +5 | 수련에 도움을 주는 갑옷입니다. | 1000G" + shop[0]);
                    Console.WriteLine("무쇠갑옷 | 방어력 +9 | 무쇠로 만들어져 튼튼한 갑옷입니다. | 2000G" + shop[1]);
                    Console.WriteLine("스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다. | 3500G" + shop[2]);
                    Console.WriteLine("낡은 검 | 공격력 +2 | 쉽게 볼 수 있는 낡은 검입니다. | 600G" + shop[3]);
                    Console.WriteLine("청동 도끼 | 공격력 +5 | 어디선가 사용됐던 것 같은 도끼입니다. | 1500G" + shop[4]);
                    Console.WriteLine("스파르타의 창 | 공격력 +7 | 스파르타의 전사들이 사용했다는 전설의 창입니다. | 3500G" + shop[5]);

                    break;
                }

                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                }

                if (input == 0)
                {
                    //다시 복사
                }

            }
        }




    }
}
