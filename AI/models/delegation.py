from sqlalchemy import Column, Integer, String, Date, Numeric
from sqlalchemy.orm import declarative_base

Base = declarative_base()

class Delegation(Base):
    __tablename__ = "DelegationIncoming"
    __table_args__ = {"schema": "dio"}

    id = Column(Integer, primary_key=True)
    Code = Column(String(255))
    Name = Column(String(255))
    Content = Column(String(255))
    IdPhongBan = Column(Integer)
    Location = Column(String(255))
    IdStaffReception = Column(Integer)
    TotalPerson = Column(Integer)
    PhoneNumber = Column(String(255))
    Status = Column(Integer)
    RequestDate = Column(Date)
    ReceptionDate = Column(Date)
    TotalMoney = Column(Numeric)

class PhongBan(Base):
    __tablename__ = "DmPhongBan"
    __table_args__ = {"schema": "hrm"}

    id = Column(Integer, primary_key=True)
    TenPhongBan = Column(String(255))

class Staff(Base):
    __tablename__ = "NsNhanSu"
    __table_args__ = {"schema": "hrm"}

    id = Column(Integer, primary_key=True)
    HoDem = Column(String(255))
    Ten = Column(String(255))
