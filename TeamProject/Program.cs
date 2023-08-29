using System.Numerics;

namespace team
{
    internal class Program
    {
        private static Character player;
        private static Item[] inventory;
        private static int ItemCount;
        private static int potionCount;
        private static List<Monster> monsters = new List<Monster>();

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        #region 초기화

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

            // 인벤토리 생성
            inventory = new Item[10];

            // 아이템 추가
            AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5));
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0));

            // 포션 갯수 추가
            potionCount = 3;

            // 몬스터 리스트
            // List<Monster> 
            monsters = new List<Monster>
            {
                new Monster("칼날부리", 1, 2, 30),
                new Monster("돌골렘", 1, 2, 20),
                new Monster("늑대", 1, 5, 20),
                new Monster("미니언", 1, 3, 20)
            };
            // 번호 부여
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Number = i + 1;
            }
        }

        #endregion

        #region 아이템 관리

        static void AddItem(Item item)
        {
            inventory[ItemCount] = item;
            ++ItemCount;
        }

        static void EquipItem(Item item)
        {
            item.IsEquiped = true;
        }

        static void UnequipItem(Item item)
        {
            item.IsEquiped = false;
        }

        static int GetItemAtkAmount()
        {
            int itemAtk = 0;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    break;

                Item curItem = inventory[i];

                if (curItem.IsEquiped)
                    itemAtk += curItem.Atk;
            }

            return itemAtk;
        }

        static int GetItemDefAmount()
        {
            int itemDef = 0;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    break;

                Item curItem = inventory[i];

                if (curItem.IsEquiped)
                    itemDef += curItem.Def;
            }

            return itemDef;
        }

        #endregion

        #region 게임 화면 출력

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전 입장");                          // 추가
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;
                case 3:                                                 //추가
                    DisplayDungeon();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            DisplayTitle("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");

            int itemAtk = GetItemAtkAmount();
            Console.Write($"공격력 :{player.Atk + itemAtk}");
            if (itemAtk != 0)
                Console.Write($"(+{itemAtk})");
            Console.WriteLine();

            int itemDef = GetItemDefAmount();
            Console.Write($"방어력 : {player.Def + itemDef}");
            if (itemDef != 0)
                Console.Write($"(+{itemDef})");
            Console.WriteLine();

            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }


        static void DisplayInventory()
        {
            Console.Clear();

            DisplayTitle("인벤토리");

            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    break;

                Item curItem = inventory[i];

                if (curItem.IsEquiped)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[E] ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write($"{curItem.Name} | ");
                if (curItem.Atk != 0) Console.Write($" 공격력 +{curItem.Atk} ");
                if (curItem.Def != 0) Console.Write($" 방어력 +{curItem.Def} ");
                Console.Write($" | {curItem.Description}");
                Console.WriteLine();
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DisplayManageEquipment();
                    break;
            }
        }

        static void DisplayManageEquipment()
        {
            Console.Clear();

            DisplayTitle("인벤토리 - 장착 관리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                    break;

                Item curItem = inventory[i];

                Console.Write($"{i + 1} ");
                if (curItem.IsEquiped)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[E] ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write($"{curItem.Name} | ");
                if (curItem.Atk != 0) Console.Write($" 공격력 +{curItem.Atk} ");
                if (curItem.Def != 0) Console.Write($" 방어력 +{curItem.Def} ");
                Console.Write($" | {curItem.Description}");
                Console.WriteLine();
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, ItemCount);
            if (input == 0)
            {
                DisplayInventory();
            }
            else if (input > 0 && input <= ItemCount)
            {

                Item curItem = inventory[input - 1];
                if (curItem.IsEquiped)
                    UnequipItem(curItem);
                else
                    EquipItem(curItem);

                DisplayManageEquipment();
            }
        }

        static void DisplayPotion()
        {
            Console.Clear();
            DisplayTitle("회복");
            Console.WriteLine("포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션: " + potionCount + ")");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                // 체력회복이 된 이후에 다시 DisplayPotion 으로 나갈 수 있도록 구현해야함.

                case 1:
                    if (potionCount > 0)
                    {
                        HealthPotion test = new HealthPotion();
                        test.Use(player);
                        Console.WriteLine("체력이 30 회복되었습니다.");
                        Console.WriteLine("현재 체력: " + player.Hp);
                        potionCount--;
                    }

                    else
                    {
                        Console.WriteLine("포션이 부족합니다.");
                    }
                    break;


                case 0:
                    DisplayGameIntro();
                    break;
            }


        }


        // 던전 화면 추가
        static void DisplayDungeon()
        {
            Console.Clear();
            Random random = new Random();

            if (monsters.Count > 0)
            {
                int numMonsters = random.Next(1, 5); // 1~4마리 랜덤으로 설정

                Console.WriteLine();
                Console.WriteLine("                 !!!");
                Console.WriteLine($"던전 안에서 총 {numMonsters}마리의 몬스터가 나타났습니다!\n");

                List<Monster> encounteredMonsters = new List<Monster>(); // 랜덤으로 뽑힌 몬스터들을 저장할 리스트

                for (int i = 0; i < numMonsters; i++)
                {
                    int randomIndex = random.Next(monsters.Count);      // 몬스터 리스트 중에 랜덤으로 뽑기
                    Monster encounteredMonster = monsters[randomIndex];     // 몬스터 정보 넣기
                    encounteredMonsters.Add(encounteredMonster); // 랜덤으로 뽑힌 몬스터를 리스트에 추가

                    Console.WriteLine($"{i + 1}. LV{encounteredMonster.Level}. {encounteredMonster.Name} (공격력: {encounteredMonster.Atk}, HP: {encounteredMonster.Hp})");

                }

                // 게임 전투 구현


                Console.WriteLine();
                Console.WriteLine("공격할 대상을 입력해주세요!!");
                Console.Write(" :   ");


                int input = CheckValidInput(1, numMonsters);
                Monster targetMonster = encounteredMonsters[input - 1]; // 선택한 몬스터

                Console.WriteLine();
                Console.WriteLine($"{targetMonster.Name}을(를) 공격합니다!");
                Console.WriteLine();

                //switch (input)
                //{
                //    case 1:
                //        // 몬스터 1  선택
                //        break;

                //    case 2:
                //        // 몬스터 2  선택
                //        break;
                //    case 3:
                //        // 몬스터 3  선택
                //        break;
                //    case 4:
                //        // 몬스터 4  선택
                //        break;
                //}

            }
            else
            {
                Console.WriteLine("던전에 몬스터가 없습니다!");
                Console.ReadKey();
                DisplayGameIntro();
            }

        }
        #endregion

        #region Utility

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                DisplayError("잘못된 입력입니다.");
            }
        }

        static void DisplayTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        static void DisplayError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        #endregion



        #region 데이터

        public class HealthPotion
        {
            public string Name => "체력포션";

            public void Use(Character healing)
            {
                Console.WriteLine("체력 포션을 사용합니다. 체력이 30 증가합니다.");
                healing.Hp += 30;
                if (player.Hp > 100) healing.Hp = 100;
            }
        }

        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; }
            public int Def { get; }
            public int Hp { get; set; }
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

            public int Atk { get; }
            public int Def { get; }

            public bool IsEquiped { get; set; }

            public Item(string name, string description, int atk, int def)
            {
                Name = name;
                Description = description;
                Atk = atk;
                Def = def;

                IsEquiped = false;
            }

        }

        public class Monster
        {
            // 몬스터 종류 List
            public int Number { get; set; }
            public string Name { get; }
            public int Level { get; }
            public int Atk { get; }
            public int Hp { get; }

            // monster 죽일 시, 골드 및 경험치
            // public int Gold { get; }
            // public int exp { get; }

            // monster>player 공격력 miss 처리
            // public int Def { get; }

            public Monster(string name, int level, int atk, int hp)
            {
                Name = name;
                Level = level;
                Atk = atk;
                Hp = hp;

            }
        }


    }


    #endregion
}   
