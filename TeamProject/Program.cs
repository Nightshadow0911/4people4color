using System;
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
            Console.WriteLine("게임을 시작합니다.");
            Console.Write("캐릭터 이름을 입력하세요: ");
            string name = Console.ReadLine();

            Console.WriteLine("직업을 선택하세요.");
            Console.WriteLine("1.전사 2.마법사 3.궁수 4.도적");

            string job = "없음";
            int jobchoose = CheckValidInput(1, 4);
            switch (jobchoose)
            {
                case 1:
                    job = "전사";
                    break;

                case 2:
                    job = "마법사";
                    break;

                case 3:
                    job = "궁수";
                    break;

                case 4:
                    job = "도적";
                    break;
            }

            int Atk = 0, Def = 0, Hp = 0;
            float Crit = 0, Evade = 0;

            Console.WriteLine("스탯을 결정해야 합니다.");
            Console.WriteLine("Enter키를 눌러 주사위를 굴려서 스탯을 정해주세요.");
            Console.ReadLine();

            bool reroll = true;
            while (reroll)
            {
                Console.WriteLine("주사위를 굴립니다.");
                Random random = new Random();
                if (job == "전사")
                {
                    Atk = random.Next(5, 12);
                    Def = random.Next(5, 10);
                    Hp = random.Next(100, 150);
                    Crit = 5;
                    Evade = 5;
                }
                else if (job == "마법사")
                {
                    Atk = random.Next(5, 15);
                    Def = random.Next(1, 3);
                    Hp = random.Next(60, 100);
                    Crit = 30;
                    Evade = 5;
                }
                else if (job == "궁수")
                {
                    Atk = random.Next(5, 10);
                    Def = random.Next(3, 7);
                    Hp = random.Next(100, 120);
                    Crit = 10;
                    Evade = 10;
                }
                else if (job == "도적")
                {
                    Atk = random.Next(3, 8);
                    Def = random.Next(3, 5);
                    Hp = random.Next(80, 100);
                    Crit = 30;
                    Evade = 30;
                }

                Console.WriteLine($"공격력: {Atk} | 방어력: {Def} | 체력: {Hp}");

                Console.Write("주사위를 다시 굴리려면 Y를 입력하세요. 다음으로 넘어가려면 다른 키를 입력하세요: ");
                reroll = Console.ReadLine().ToUpper() == "Y";
            

            // 캐릭터 정보 세팅
            player = new Character(name, job, 1, Atk, Def, Hp, Crit, Evade, 1500);
            }

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
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전 입장");                          // 추가
            Console.WriteLine("4. 회복 아이템");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 4);
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

                case 4:
                    DisplayPotion();
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
            Console.WriteLine($"크리티컬: {player.Crit}%");
            Console.WriteLine($"회피: {player.Evade}%");
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

            if (input == 0)
            {
                DisplayGameIntro();
            }

            if (input == 1)
            {
                if (potionCount > 0)
                {
                    HealthPotion test = new HealthPotion();
                    test.Use(player);
                    Console.WriteLine("체력이 30 회복되었습니다.");
                    Console.WriteLine("현재 체력: " + player.Hp);
                    potionCount--;
                    Console.WriteLine();
                    Console.WriteLine("0. 뒤로가기");

                    int n = CheckValidInput(0, 0);

                    switch (n)
                    {
                        case 0:
                            DisplayPotion();
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("포션이 부족합니다.");
                    Console.WriteLine();
                    Console.WriteLine("0. 뒤로가기");

                    int n = CheckValidInput(0, 0);

                    switch (n)
                    {
                        case 0:
                            DisplayPotion();
                            break;
                    }
                }
            }


        }


        // 던전 화면 추가
        static void DisplayDungeon()
        {
            Console.Clear();
            Random random = new Random();
            // 던전에 몬스터가 있으면
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
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------- 수정 할 부분 나눔

                int MonsterLife = numMonsters;                                  // 살아있는 몬스터 수를 변수 새로 지정

                List<Monster> monsterInstances = new List<Monster>();           // 마주친 몬스터 중 살아있는 몬스터 List

                foreach (var encounteredMonster in encounteredMonsters)         // 마주친 몬스터들을 차례로 불러오기
                {
                    Monster newMonster = new Monster(encounteredMonster.Name, encounteredMonster.Level, encounteredMonster.Atk, encounteredMonster.Hp); // 몬스터 복사 생성
                    monsterInstances.Add(newMonster);
                }

                List<bool> isMonsterAlive = Enumerable.Repeat(true, monsterInstances.Count).ToList();       // true의 값을 살아있는 몬스터 수 만큼 반복해서, 리스트에 넣기 (어려운 함수)

                // 몬스터가 남아있으면 실행
                while (MonsterLife > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("공격할 대상을 입력해주세요!!     (0. 도망가기)");

                    Console.Write(" :   ");


                    // 도망가기(게임 인트로로)
                    int input = CheckValidInput(0, numMonsters);
                    switch (input)
                    {
                        case 0:
                            DisplayGameIntro();
                            break;
                    }


                    // 몬스터 등장

                    Monster targetInstance = monsterInstances[input - 1]; // 선택한 몬스터
                    int targetIndex = monsterInstances.IndexOf(targetInstance);
                    Console.WriteLine();
                    Console.WriteLine($"{targetInstance.Name}을(를) 공격합니다!");
                    Console.WriteLine();
                    int itemAtk = GetItemAtkAmount();           // 플레이어 공격력 불러오기

                    // flase 값을 불러와 맞으면 출력
                    if (!isMonsterAlive[targetIndex])
                    {
                        Console.WriteLine("이미 죽은 몬스터입니다. 다른 몬스터를 선택하세요.");
                        continue;
                    }

                    // 깨끗하게 지워주기
                    Console.Clear();

                    if (targetInstance.Hp > 0)       // 선택한 몬스터가 살아있으면
                    {
                        int MonsterDamage = player.Atk + itemAtk;
                        targetInstance.Hp -= MonsterDamage;       // 플레이어 공격력 적용
                        Console.WriteLine();
                        Console.WriteLine($"{targetInstance.Name}에게 데미지를 입혔습니다!");
                        Console.WriteLine();
                        if (targetInstance.Hp <= 0)
                        {
                            Console.WriteLine($"{targetInstance.Name} 잡았다!");
                            Console.WriteLine();

                            isMonsterAlive[targetIndex] = false;        // false 값 처리
                            MonsterLife--;                              // 몬스터 수 처리
                        }

                        // 플레이어 공격 위치??????
                    }

                    // 데미지 얻은 몬스터 업데이트
                    for (int j = 0; j < monsterInstances.Count; j++)
                    {
                        // 살아 있는 몬스터를 남은 몬스터로 지정(새 변수)
                        Monster remainingMonster = monsterInstances[j];

                        // True의 값
                        if (isMonsterAlive[j])
                        {
                            Console.WriteLine($"{j + 1}. LV{remainingMonster.Level}. {remainingMonster.Name} (공격력: {remainingMonster.Atk}, HP: {remainingMonster.Hp})");
                        }
                    }

                    // 플레이어 공격 위치??????

                    // 몬스터를 모두 해치우면
                    if (MonsterLife <= 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("플레이어 승리!");
                        Console.WriteLine("키를 누르면, 되돌아갑니다.");
                        Console.ReadKey();
                        DisplayGameIntro();
                        break;
                    }
                }

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
            public int Level { get; set; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int Hp { get; set; }
            public float Crit { get; set; }
            public float Evade { get; set; }
            public int Gold { get; }

            public Character(string name, string job, int level, int atk, int def, int hp, float crit, float evade, int gold)
            {
                Name = name;
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Crit = crit;
                Evade = evade;
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
            public int Crit { get; set; }
            public int Evade { get; set; }


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
