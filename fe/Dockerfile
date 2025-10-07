# Stage 1: Build ứng dụng Next.js
FROM node:lts-alpine AS builder
WORKDIR /app

# Copy file package.json và cài đặt dependencies
COPY package*.json ./
RUN npm install

# Copy toàn bộ source code
COPY . .

# Build ứng dụng production
RUN npm run build

# Stage 2: Chạy ứng dụng Next.js
FROM node:lts-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production

# Copy file cần thiết từ stage build
COPY --from=builder /app/package*.json ./
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public
COPY --from=builder /app/node_modules ./node_modules

# Mặc định Next.js chạy ở port 3000
EXPOSE 3000

# Lệnh khởi động app
CMD ["npm", "start"]
