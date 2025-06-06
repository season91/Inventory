public enum MainButtonType
{
    Status,
    Inventory,
    Back
}

public interface IGUI
{
    // 초기화
    public void Initialization();
    
    // 열기
    public void Open();
    
    // 닫기
    public void Close();
}
