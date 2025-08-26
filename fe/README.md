This is a [Next.js](https://nextjs.org) project bootstrapped with [`create-next-app`](https://nextjs.org/docs/app/api-reference/cli/create-next-app).

## Requirement

Node 20  
Next 14  
Antd v5

## Getting Started

Cài đặt thư viện:

```bash
npm install
# or
yarn
```

Chạy dự án lên môi trường dev:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3077](http://localhost:3077) with your browser to see the result.

This project uses [`next/font`](https://nextjs.org/docs/app/building-your-application/optimizing/fonts) to automatically optimize and load [Geist](https://vercel.com/font), a new font family for Vercel.

## Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js) - your feedback and contributions are welcome!

## Deploy on Vercel

The easiest way to deploy your Next.js app is to use the [Vercel Platform](https://vercel.com/new?utm_medium=default-template&filter=next.js&utm_source=create-next-app&utm_campaign=create-next-app-readme) from the creators of Next.js.

Check out our [Next.js deployment documentation](https://nextjs.org/docs/app/building-your-application/deploying) for more details.

---

## 📁 Thư mục `app/` trong Next.js (App Router)

Thư mục `app/` là nơi định nghĩa cấu trúc routing và layout của ứng dụng sử dụng App Router của Next.js. Dưới đây là mô tả chức năng của từng file quan trọng trong `app/`.

---

### 📄 `layout.tsx`

- **Vai trò:** Định nghĩa layout bao ngoài cho toàn bộ các trang con trong route hiện tại.
- **Đặc điểm:**
  - Được render **một lần duy nhất**, không bị remount khi điều hướng giữa các trang con.
  - Thường chứa các thành phần cố định như: `Navbar`, `Footer`, `Sidebar`, cấu hình font, theme...
  - Không nên dùng cho logic cần chạy lại mỗi lần chuyển trang.

---

### 📄 `loading.tsx`

- **Vai trò:** Hiển thị giao diện chờ (loading UI) trong khi dữ liệu hoặc component đang được load (async).
- **Đặc điểm:**
  - Tự động được dùng khi có `React.lazy` hoặc `suspense`.
  - Thường dùng để hiển thị spinner, skeleton UI...

---

### 📄 `error.tsx`

- **Vai trò:** Hiển thị giao diện lỗi khi xảy ra exception (lỗi render, lỗi fetch...) trong quá trình render trang.
- **Đặc điểm:**
  - Có thể dùng để hiển thị thông báo lỗi thân thiện với người dùng.
  - Hỗ trợ `reset()` để cho phép người dùng thử lại.

---

### 📄 `not-found.tsx`

- **Vai trò:** Hiển thị giao diện khi không tìm thấy route hoặc dữ liệu.
- **Đặc điểm:**
  - Tự động hiển thị khi bạn gọi `notFound()` từ server component.
  - Hoặc khi người dùng truy cập đường dẫn không hợp lệ.

---

### 📄 `page.tsx`

- **Vai trò:** Là trang chính (default route) tương ứng với URL của thư mục chứa nó.
- **Đặc điểm:**
  - Là nơi bạn render nội dung chính của route hiện tại.
  - Có thể là `server component` hoặc `client component` (tuỳ vào nhu cầu).

---

### 📄 `template.tsx`

- **Vai trò:** Tương tự như `layout.tsx`, dùng để bao ngoài các `page`, nhưng **khác biệt ở chỗ nó sẽ được mount lại mỗi lần điều hướng sang trang mới**.
- **Khi nào nên dùng:**
  - Cần chạy lại logic như `useEffect` (ví dụ: log xem trang, đo thời gian).
  - Mỗi trang cần có state độc lập (ví dụ: form phản hồi, toggle UI riêng).
  - Muốn `Suspense fallback` hiển thị **mỗi lần chuyển trang**, không chỉ lần đầu.
- **Đặc điểm:**
  - Không được "cache lại" như `layout.tsx`.
  - Hữu ích cho các layout động.

📌 **Lưu ý:** `template.tsx` không thay thế `layout.tsx`, mà chỉ dùng song song nếu bạn cần hành vi "remount mỗi lần điều hướng".
