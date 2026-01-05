# 🚀 LỘ TRÌNH 90 NGÀY – HỌC & BUILD DỰ ÁN HRM (NOVA HR)

**Mục tiêu:** Update level từ Fresher lên Junior.
**Tiến độ hiện tại:** Đang ở **PHẦN 2 (Day 8)**.

---

## ✅ PHẦN 0 – BA + ARCHITECTURE (Day 0)
- [x] Thu thập yêu cầu
- [x] Use case
- [x] Workflow
- [x] ERD
- [x] Domain model
- [x] Kiến trúc
- [x] Naming rules
- [x] ID rules

## ✅ PHẦN 1 — C# FOUNDATION (Day 1 → Day 7)
**Mục tiêu:** Hoàn thành Domain Entities + Logic cơ bản
- [x] **Day 1**: Struct, Class, Object | Task: Entities (Employee, Department, Position)
- [x] **Day 2**: Constructor, Property, Encap | Task: Entity Shift + AttendanceRecord
- [x] **Day 3**: Method, Val/Ref Types | Task: Business logic (AttendanceRecord.IsLate/Calculate)
- [x] **Day 4**: Control Flow | Task: Logic checking (Validate shift, check-in/out)
- [x] **Day 5**: Inheritance, Polymorphism | Task: BaseEntity (Id, CreatedAt...)
- [x] **Day 6**: Abstraction, Interface cơ bản | Task: AttendanceCalculator
- [x] **Day 7**: Review Clean Code, Domain Architecture

## 🚧 PHẦN 2 — C# ADVANCED (Day 8 → 16)
**Mục tiêu:** Hoàn thiện Payroll Engine (Strategy Pattern)
- [ ] **Day 8**: Interface sâu hơn | Task: IPayrollRule
- [ ] **Day 9**: Abstract class | Task: PayrollRuleBase
- [ ] **Day 10**: Generics | Task: IRepository<T>
- [ ] **Day 11**: IDisposable | Task: BaseRepository Interface
- [ ] **Day 12**: Extension methods | Task: DateTimeExtensions
- [ ] **Day 13**: Strategy pattern | Task: RuleOvertime
- [ ] **Day 14**: Task: RuleLatePenalty
- [ ] **Day 15**: Task: RuleKPIScore
- [ ] **Day 16**: Build PayrollEngine + Unit Tests

## 🌐 PHẦN 3 — ASP.NET CORE BASIC (Day 17 → 23)
**Mục tiêu:** CRUD API chuẩn doanh nghiệp
- [ ] **Day 17**: Routing | Task: GET /employees
- [ ] **Day 18**: Model binding | Task: POST /employees
- [ ] **Day 19**: Validation | Task: Validate salary, email...
- [ ] **Day 20**: ActionResult | Task: PUT /employees/{id}
- [ ] **Day 21**: HTTP Verbs | Task: DELETE employee
- [ ] **Day 22**: Task: CRUD Department, Shift
- [ ] **Day 23**: Review & Refactor API

## 🧩 PHẦN 4 — DEPENDENCY INJECTION (Day 24 → 27)
**Mục tiêu:** Inject service đúng chuẩn
- [ ] **Day 24**: Lifecycle (Scoped, Transient, Singleton)
- [ ] **Day 25**: Task: IAttendanceService
- [ ] **Day 26**: Task: IShiftService
- [ ] **Day 27**: Task: ISalaryService + Integration

## ⚙️ PHẦN 5 — MIDDLEWARE (Day 28 → 32)
**Mục tiêu:** Logging + Exception handling
- [ ] **Day 28**: Pipeline concept
- [ ] **Day 29**: Task: LoggingMiddleware
- [ ] **Day 30**: Task: ExceptionMiddleware
- [ ] **Day 31**: Format JSON error response
- [ ] **Day 32**: Review + Demo middleware

## 🗃️ PHẦN 6 — DATABASE + SQL (Day 33 → 40)
**Mục tiêu:** DB Diagram + SQL tối ưu
- [ ] **Day 33**: Thiết kế ERD lần 1
- [ ] **Day 34**: Join Queries
- [ ] **Day 35**: Group-by (Payroll monthly)
- [ ] **Day 36**: Indexing (Attendance table)
- [ ] **Day 37**: Task: Query lương tháng
- [ ] **Day 38**: Task: Overtime summary
- [ ] **Day 39**: Task: Nhân viên theo phòng ban
- [ ] **Day 40**: Finalize ERD

## 🛠️ PHẦN 7 — EF CORE (Day 41 → 50)
**Mục tiêu:** Repository + CRUD chuẩn
- [ ] **Day 41**: Migration + DbContext
- [ ] **Day 42**: Employees CRUD
- [ ] **Day 43**: Attendance CRUD
- [ ] **Day 44**: Payroll CRUD
- [ ] **Day 45**: Leave Request CRUD
- [ ] **Day 46**: Include + ThenInclude
- [ ] **Day 47**: NoTracking tối ưu
- [ ] **Day 48**: Transaction (Payroll batch)
- [ ] **Day 49**: Rollback handling
- [ ] **Day 50**: Review EF Performance

## 🔐 PHẦN 8 — AUTH (Day 51 → 56)
**Mục tiêu:** JWT + Role Permission
- [ ] **Day 51**: JWT Setup
- [ ] **Day 52**: Role-based (Admin/HR/Manager/Employee)
- [ ] **Day 53**: Login API
- [ ] **Day 54**: Middleware authorize policy
- [ ] **Day 55**: Manager duyệt phép
- [ ] **Day 56**: HR tạo payroll

## 🧱 PHẦN 9 — SOLID (Day 57 → 60)
**Mục tiêu:** Refactor chuẩn SOLID
- [ ] **Day 57**: SRP (Attendance)
- [ ] **Day 58**: OCP (PayrollRule mở rộng)
- [ ] **Day 59**: DIP (SalaryService)
- [ ] **Day 60**: Code Review & Clean

## 🧩 PHẦN 10 — DESIGN PATTERNS (Day 61 → 67)
**Mục tiêu:** 7 patterns trong HRM
- [ ] **Day 61**: Strategy (Lương)
- [ ] **Day 62**: Factory (Shift rules)
- [ ] **Day 63**: Repository + UoW
- [ ] **Day 64**: Decorator (Logging)
- [ ] **Day 65**: Adapter (QR Scanner)
- [ ] **Day 66**: Facade (Payroll Summary)
- [ ] **Day 67**: Review Patterns

## 🏛️ PHẦN 11 — CLEAN ARCHITECTURE (Day 68 → 75)
**Mục tiêu:** Tách dự án thành 4-layer
- [ ] **Day 68-69**: Create 4 projects (Domain, App, Infra, API)
- [ ] **Day 70-71**: Move Entities & Rules to Domain
- [ ] **Day 72-73**: Application Layer (DTO, UseCases)
- [ ] **Day 74-75**: Infrastructure Layer (EF, Config)

## 🚀 PHẦN 12 — HRM SYSTEM V1.0 (Day 76 → 85)
**Mục tiêu:** Build sản phẩm hoàn thiện
- [ ] Chấm công (QR + manual)
- [ ] Payroll full rule
- [ ] Leave Request
- [ ] Shift
- [ ] Role-based Access
- [ ] Dashboard
- [ ] Export Excel/PDF
- [ ] Logs
- [ ] Error middleware

## 🖥️ PHẦN 13 — DEVOPS (Day 86 → 90)
- [ ] **Day 86**: Dockerfile API
- [ ] **Day 87**: Docker Compose (+SQL)
- [ ] **Day 88**: GitHub Actions CI
- [ ] **Day 89**: Auto Deploy
- [ ] **Day 90**: CV + Portfolio + Demo Video
