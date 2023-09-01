# 4people4color
# 프로젝트 소개
던전에 도착한 캐릭터가 전투를 하는 게임을 텍스트로 구현합니다.

여러 몬스터들과 상호작용하는 턴제방식을 채용합니다.

# 개발 기간
2023.08.28 ~ 2023.09.01

# 멤버 구성 및 역할 분담

조병우 : 캐릭터, 직업생성, 치명타, 회피 설정, 전투기능(몬스터 공격)


김경환 : 게임 저장 기능


김민석 : 던전결과 레벨업&보상, 회복아이템 구현


이승연 : 전투씬 몬스터 등장 구현, 전투기능(플레이어 공격)


# 게임 하는 방법

구현 목록
-
필수 요구 사항
- 게임 시작 화면
- 상태보기
- 전투시작(공격+결과)

선택 요구 사항
- 캐릭터 생성 기능
- 직업 선택 기능
DisplayCharacterCreate에서 직업을 선택할 수 있습니다.
  직업을 4종류 중 하나를 고르면 스탯을 정하는 기능으로 넘어가는데 각 직업에 따라 능력치 폭이 다르도록 설정되어 있어 원하는 스탯을 정할때까지 재시도할 수 있습니다.
  
- 치명타 기능
- 회피 기능
DisplayCharacterCreate에서 능력치 중에 치명타와 회피 확률이 있으며, DisplayDungeon에 CaclculateDamage에서 플레이어의 공격력에서 몬스터의 방어력을 뺀 값을
  if (random.Next(100) < player.Crit)을 통해 확률이 발동하면 치명타가 터지게 되고
  회피 기능 또한 CanEvade에서]
  bool CanEvade(float evade)
{
    return random.Next(100) < evade;
}
을 통해 회피할 수 있습니다.
- 레벨업 기능
- 보상 추가
- 회복 아이템
- 게임 저장하기
