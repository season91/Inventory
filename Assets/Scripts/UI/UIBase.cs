using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public abstract class UIBase : MonoBehaviour
{
    // CanvasGroup은 UI의 투명도(alpha), 상호작용 가능 여부(Raycast) 등을 제어함
    [SerializeField] protected CanvasGroup canvasGroup;

    // 실제 애니메이션을 적용할 UI Panel (보통은 전체 패널의 RectTransform)
    [SerializeField] protected RectTransform panelRoot;

    // 애니메이션 재생 시간 설정
    protected float openDuration = 0.3f;
    protected float closeDuration = 0.2f;

    protected bool isInitialized = false; // 초기화 확인 변수
    
    [SerializeField] protected Button btnBack;
    
    
    // Initialization: UI 최초 준비 (한 번만 실행)
    public virtual void Initialization()
    {
        if (isInitialized) return;

        canvasGroup.alpha = 0;
        panelRoot.localScale = Vector3.zero;
        gameObject.SetActive(false);
        
        isInitialized = true;
    }

    public virtual void Open() => Open(false);  // 기본은 뒤로가기 버튼 숨김
    
    public virtual void Open(bool showBackButton)
    {
        // 1. 우선 GameObject 자체를 활성화함 (SetActive true)
        gameObject.SetActive(true);

        // 2. 초기 상태 세팅: 투명하고 축소된 상태로 시작
        canvasGroup.alpha = 0;
        panelRoot.localScale = Vector3.zero;
        canvasGroup.blocksRaycasts = false; // 클릭도 막아둠 (애니메이션 도중)

        // 3. 애니메이션 시작: 투명도 1, 크기 1로 확대
        canvasGroup.DOFade(1f, openDuration).SetUpdate(true);
        panelRoot.DOScale(Vector3.one, openDuration)
                 .SetEase(Ease.OutBack)
                 .SetUpdate(true)
                 .OnComplete(() => canvasGroup.blocksRaycasts = true);// 애니메이션 끝나면 클릭 허용
        
        // 4. 뒤로가기 버튼 처리
        SetBackButtonVisible(showBackButton);
    }

    public virtual void Close() => Close(false);  // 기본은 뒤로가기 버튼 숨김
    
    // UI 닫기 함수 (인벤토리 등 숨길 때 사용)
    public virtual void Close(bool showBackButton)
    {
        // 1. 닫을 때는 즉시 클릭 막기
        canvasGroup.blocksRaycasts = false;

        canvasGroup.DOFade(0f, closeDuration).SetUpdate(true);
        panelRoot.DOScale(Vector3.zero, closeDuration)
                 .SetEase(Ease.InBack) // 부드럽게 작아지며 사라짐
                 .SetUpdate(true)
                 .OnComplete(() =>
                 {
                     gameObject.SetActive(false);
                 });
        
    }
    
    private void SetBackButtonVisible(bool visible)
    {
        if (btnBack != null)
            btnBack.gameObject.SetActive(visible);
    }
}
