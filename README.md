# Inventory
<img width="800" alt="image" src="https://github.com/user-attachments/assets/8e15c716-40c6-40ed-9b95-e686e68a6f10" />
저희집 강아지를 캐릭터로 삼아 프림이 가방을 구현했습니다. 주로 먹을 것만 들어 있습니다.

# Scripts 폴더 구조 안내
Unity 프로젝트의 스크립트 구조입니다. 기능별로 폴더를 분리하여 복습 위주로 공부하며 개발했습니다.

```
Scripts/
├── Item/
│ ├── ItemData.cs # 아이템 데이터 정의 (ScriptableObject)
│ └── ItemObject.cs # 월드에 존재하는 아이템 프리팹 스크립트
│
├── Manager/
│ ├── GameManager.cs # 게임 전반 관리 (상태, 흐름 등)
│ └── UIManager.cs # UI 열고 닫기, 전환 제어
│
├── Player/
│ └── Player.cs # 플레이어 속성 및 이동 로직
│
├── UI/
│ ├── Canvas/
│ │ ├── UICanvasInventory.cs # 인벤토리 전체 UI
│ │ ├── UICanvasMainMenu.cs # 메인 메뉴 UI
│ │ ├── UICanvasMenuBtn.cs # 메뉴 버튼 처리
│ │ └── UICanvasStatus.cs # 캐릭터 상태창
│ │
│ ├── Inventory/
│ │ └── UIInventorySlot.cs # 인벤토리 한 칸 UI
│ │
│ ├── MainMenu/
│ │ ├── UICharactorInfo.cs # 캐릭터 정보 창
│ │ ├── UIJellyInfo.cs # 젤리 관련 정보 UI
│ │ └── UITooltip.cs # 툴팁 표시 UI
│ │
│ └── UIBase.cs # 공통 UI 베이스 클래스
│
└── Utils/
  └── CSVImporter.cs # CSV 파일로 데이터 불러오기 (아이템 등)
```
---

