# DesLike
2022 공모전 준비작

일정

기획)
1. 식량 자원이 필요한가? 라운드 제도가 없으면 식량이랑 골드를 통합해도 될 것 같음
2. 스토리짜기
3. 진영, 영웅, 유물, 이벤트 개발

백엔드)-녕준
1. DataSheet 저장할 외부 저장공간 만들기(CSV 테이블? 보류)
2. 상태이상 표시

MainTitle)-녕준
1. 사전 기능 보완(상세 정보, 클래스 별로 다른 패널 보여주기, 발견 여부에 따라 표시 다르게 하기)
2. 옵션 설정 만들기(GameData에다가 값들 저장해놓고 불러오기)
3. 배경 음악 등 WWise 혹은 다른 방법으로 넣을 방법 공부하기
4. Continue에서 받아올 GameData 정리

Map)-시후
1. 맵 스테이지 별 특성 정하기
2. 맵 스크립터블 오브젝트 GameData에 넘겨주는 함수 만들기
3. 도전모드 활성화
4. 적 병사 미리보기
5. 현재 병력을 그냥 본거지에서 확인하면 될 것 같은데 위에 메뉴바에 필요할까?
6. 일 수를 fillAmount로 디자인적으로 변경
7. 배틀노드에서 적 정보가 정확하게 뜨지 않음, More 누르면 터지고, 터진 상태에서 그 노드를 플레이할 수 없음
8. 리워드패널에 end 버튼에 endMapNode 함수 넣어야함
9. selectMap script 113~114 줄 food 지우면서 오류 떠서 주석처리 해놓음

본거지)-녕준
1. 진영효과 패널 추가하기
2. 포트 관련 업그레이드(본거지에서 할 것인가?)
3. 포트에서 바둑판 식으로 유닛들 미리 배치 가능하게 만들기

전투)-녕준
1. 미리 유닛들 스폰해놓고 시작하기
2. 병력비를 나타내는 바 하나 추가하기
3. 필요없어진 UI들 제거해주기
4. 배속 기능 추가하기(스킬 쓰는 동안은 0.1배속이나 0배속으로, 일시정지 중에는 못 쓰게)

마을)-미정
1. 머든 해야함 ㅋㅋ

아트)
팀 일단 구하기

병사 Resource 생산시 설정해줘야하는 것
1. 적절한 Behaviour 붙여주고 SoldierInfo 할당해주기
2. 평타나 스킬 붙여주기
3. SoldierInfo에 code 설정해주기
4. SoldierInfo에 Dead Event에 Behaviour StopAllCoroutine 추가해주기
5. 콜라이더 크기 수정해주기(수정하고 soldierData에 PhysicCollider 반지름 size에 넣어주기)
6. SpriteRenderer에 대표 이미지 설정해주기(선택)
