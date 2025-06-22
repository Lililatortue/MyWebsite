create table web_user(
  id        SERIAL PRIMARY KEY,
  username  TEXT NOT NULL,
  email     TEXT UNIQUE NOT NULL,
  password  TEXT NOT NULL,
  role      TEXT NOT NULL CHECK (role in ('guest', 'admin', 'super')),
  isbanned  BOOLEAN DEFAULT false
)
