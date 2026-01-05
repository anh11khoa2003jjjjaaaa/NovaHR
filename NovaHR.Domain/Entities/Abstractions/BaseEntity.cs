using NovaHR.Domain.Extensions;

namespace NovaHR.Domain.Interfaces
{
    // BaseEntity đại diện cho khái niệm "thực thể nghiệp vụ của hệ thống"
    // Mọi entity trong domain đều có định danh (Id)
    // ==> User, Role, Account... đều LÀ một BaseEntity (quan hệ IS-A)
    // ==> Dùng kế thừa để chia sẻ định danh chung
    // ==> BaseEntity là khái niệm trừu tượng, không tồn tại độc lập
    // ==> Dùng abstract không cho phép tạo instance trực tiếp từ BaseEntity
    // ==> Lý do dùng abstract là có chứa state chung là Id cho tất cả các entity

    //_________________________________CÁC BƯỚC XÁC ĐỊNH_________________________________//
    // Bước 1: Kiểm tra nó có phải là IS-A không?
    //  + Nếu là IS-A thì dùng INHERITANCE (kế thừa)
    //  + Nếu là CAN-DO thì dùng INTERFACE (interface/contract)
    // Bước 2: Xem xét có nên dùng abstract class không?
    //  + Nếu class cha có giữ state chung, logic, cho tất cả các entity trong Domain thì DÙNG
    //  + Nếu không thì không dùng

    public abstract class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

    }
}
