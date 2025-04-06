### Предварительные требования
- Docker
- Docker Compose

### Установка и запуск
```bash
git clone https://github.com/Olewwwka/LibraryWeb
cd LibraryWeb
docker-compose up --build
```

После запуска приложение будет доступно:
- **API**: http://localhost:5000
- **Frontend**: http://localhost:4200
- **Swagger UI**: http://localhost:5000/swagger/index.html

## 🔐 Тестовые пользователи
| Роль       | Email              | Пароль    |
|------------|--------------------|-----------|
| Администратор | admin@library.com | Admin123! |
| Пользователь   | user@library.com  | User123!  |
