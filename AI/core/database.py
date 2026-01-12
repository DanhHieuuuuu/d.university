from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
import urllib

params = urllib.parse.quote_plus(
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=203.210.148.167,8092;"
    "DATABASE=DO_AN;"
    "UID=sa;"
    "PWD=labDev@123;"
    "TrustServerCertificate=yes;"
    "MultipleActiveResultSets=True;"
)

DATABASE_URL = f"mssql+pyodbc:///?odbc_connect={params}"

engine = create_engine(
    DATABASE_URL,
    echo=False
)

SessionLocal = sessionmaker(
    autocommit=False,
    autoflush=False,
    bind=engine
)
