# 🛠️ Hướng dẫn sửa chức năng Xóa cho các trang Đào tạo

## ⚠️ Vấn đề hiện tại

Tất cả các trang trong module **Đào tạo** đều có button "Xóa" nhưng **không hoạt động** vì chưa gọi API delete.

## ✅ Đã sửa

- ✅ **major/page.tsx** (Ngành) - Hoàn thành

## 🔧 Cần sửa (4 trang)

1. **faculty/page.tsx** (Khoa)
2. **specialization/page.tsx** (Chuyên ngành)
3. **course/page.tsx** (Môn học)
4. **prerequisiteCourse/page.tsx** (Môn học tiên quyết)

---

## 📋 Template sửa lỗi

### Bước 1: Thêm imports

```typescript
// TRƯỚC:
import { Button, Card, Form, Input } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined
} from '@ant-design/icons';

// SAU:
import { Button, Card, Form, Input, Modal, message } from 'antd';
import {
  PlusOutlined,
  SearchOutlined,
  SyncOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeOutlined,
  ExclamationCircleOutlined
} from '@ant-design/icons';
```

### Bước 2: Import delete thunk

| Trang                  | Import cần thêm                                                                                   |
| ---------------------- | ------------------------------------------------------------------------------------------------- |
| **faculty**            | `import { getAllKhoa, deleteKhoa } from '@redux/feature/dao-tao/khoaThunk';`                      |
| **specialization**     | `import { getAllChuyenNganh, deleteChuyenNganh } from '@redux/feature/dao-tao/chuyenNganhThunk';` |
| **course**             | `import { getAllMonHoc, deleteMonHoc } from '@redux/feature/dao-tao/monHocThunk';`                |
| **prerequisiteCourse** | Chưa có delete thunk - cần tạo                                                                    |

### Bước 3: Thêm hàm handleDelete

Thêm sau hàm `refreshData()`:

#### Faculty (Khoa):

```typescript
const handleDelete = (id: number, tenKhoa: string) => {
  Modal.confirm({
    title: 'Xác nhận xóa',
    icon: <ExclamationCircleOutlined />,
    content: `Bạn có chắc chắn muốn xóa khoa "${tenKhoa}"?`,
    okText: 'Xóa',
    okType: 'danger',
    cancelText: 'Hủy',
    onOk: async () => {
      try {
        await dispatch(deleteKhoa(id)).unwrap();
        message.success('Xóa khoa thành công!');
        refreshData();
      } catch (error: any) {
        message.error(error?.message || 'Xóa khoa thất bại!');
      }
    }
  });
};
```

#### Specialization (Chuyên ngành):

```typescript
const handleDelete = (id: number, tenChuyenNganh: string) => {
  Modal.confirm({
    title: 'Xác nhận xóa',
    icon: <ExclamationCircleOutlined />,
    content: `Bạn có chắc chắn muốn xóa chuyên ngành "${tenChuyenNganh}"?`,
    okText: 'Xóa',
    okType: 'danger',
    cancelText: 'Hủy',
    onOk: async () => {
      try {
        await dispatch(deleteChuyenNganh(id)).unwrap();
        message.success('Xóa chuyên ngành thành công!');
        refreshData();
      } catch (error: any) {
        message.error(error?.message || 'Xóa chuyên ngành thất bại!');
      }
    }
  });
};
```

#### Course (Môn học):

```typescript
const handleDelete = (id: number, tenMonHoc: string) => {
  Modal.confirm({
    title: 'Xác nhận xóa',
    icon: <ExclamationCircleOutlined />,
    content: `Bạn có chắc chắn muốn xóa môn học "${tenMonHoc}"?`,
    okText: 'Xóa',
    okType: 'danger',
    cancelText: 'Hủy',
    onOk: async () => {
      try {
        await dispatch(deleteMonHoc(id)).unwrap();
        message.success('Xóa môn học thành công!');
        refreshData();
      } catch (error: any) {
        message.error(error?.message || 'Xóa môn học thất bại!');
      }
    }
  });
};
```

### Bước 4: Sửa action "Xóa"

Trong mảng `actions`, tìm action có `label: 'Xóa'` và sửa `command`:

#### TRƯỚC:

```typescript
{
  label: 'Xóa',
  color: 'red',
  icon: <DeleteOutlined />,
  command: (record: IViewXxx) => {
    dispatch(setSelectedIdXxx(record.id));  // ❌ Chỉ set ID, không xóa
  }
}
```

#### SAU (Faculty):

```typescript
{
  label: 'Xóa',
  color: 'red',
  icon: <DeleteOutlined />,
  command: (record: IViewKhoa) => {
    handleDelete(record.id, record.tenKhoa);  // ✅ Gọi hàm xóa
  }
}
```

#### SAU (Specialization):

```typescript
{
  label: 'Xóa',
  color: 'red',
  icon: <DeleteOutlined />,
  command: (record: IViewChuyenNganh) => {
    handleDelete(record.id, record.tenChuyenNganh);
  }
}
```

#### SAU (Course):

```typescript
{
  label: 'Xóa',
  color: 'red',
  icon: <DeleteOutlined />,
  command: (record: IViewMonHoc) => {
    handleDelete(record.id, record.tenMonHoc);
  }
}
```

---

## ⚠️ Trường hợp đặc biệt: prerequisiteCourse

Trang **Môn học tiên quyết** có thể **không có delete thunk**. Cần:

1. Kiểm tra xem có `monHocTienQuyetThunk.ts` không
2. Kiểm tra có API `deleteMonHocTienQuyet` trong service không
3. Nếu chưa có → Tạo thunk và API service

---

## 🎉 Kết quả sau khi sửa

- ✅ Click "Xóa" → Hiện modal xác nhận
- ✅ Click "Xóa" trong modal → Gọi API delete
- ✅ Thành công → Hiện message success + reload data
- ✅ Thất bại → Hiện message error

---

## 🔍 Checklist cho mỗi trang

- [ ] Import `Modal`, `message` từ `antd`
- [ ] Import `ExclamationCircleOutlined`
- [ ] Import `deleteXxx` thunk
- [ ] Thêm hàm `handleDelete()`
- [ ] Sửa `command` trong action "Xóa"
- [ ] Test trên UI
