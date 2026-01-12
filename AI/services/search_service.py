from core.database import SessionLocal
from models.delegation import Delegation, PhongBan, Staff
from sqlalchemy.orm import Session

def search_delegation_incoming(conditions: dict):
    db: Session = SessionLocal()

    try:
               # Join bảng PhongBan và Staff
        query = db.query(
            Delegation,
            PhongBan.TenPhongBan.label("PhongBanName"),
            (Staff.HoDem + " " + Staff.Ten).label("StaffFullName")
        ).outerjoin(
            PhongBan, Delegation.IdPhongBan == PhongBan.id
        ).outerjoin(
            Staff, Delegation.IdStaffReception == Staff.id
        )

        if conditions.get("code"):
            query = query.filter(
                Delegation.Code.ilike(f"%{conditions['code']}%")
            )

        if conditions.get("name"):
            query = query.filter(
                Delegation.Name.ilike(f"%{conditions['name']}%")
            )

        if conditions.get("status") is not None:
            query = query.filter(
                Delegation.Status == conditions["status"]
            )
        
        if conditions.get("id_phong_ban"):
            query = query.filter(Delegation.IdPhongBan == conditions["id_phong_ban"])

        if conditions.get("id_staff_reception"):
            query = query.filter(Delegation.IdStaffReception == conditions["id_staff_reception"])

        if conditions.get("id_phong_ban"):
            query = query.filter(
                Delegation.IdPhongBan == conditions["id_phong_ban"]
            )

        if conditions.get("id_staff_reception"):
            query = query.filter(
                Delegation.IdStaffReception == conditions["id_staff_reception"]
            )

        if conditions.get("from_request_date"):
            query = query.filter(
                Delegation.RequestDate >= conditions["from_request_date"]
            )

        if conditions.get("to_request_date"):
            query = query.filter(
                Delegation.RequestDate <= conditions["to_request_date"]
            )

        if conditions.get("from_reception_date"):
            query = query.filter(
                Delegation.ReceptionDate >= conditions["from_reception_date"]
            )

        if conditions.get("to_reception_date"):
            query = query.filter(
                Delegation.ReceptionDate <= conditions["to_reception_date"]
            )

        results = query.order_by(
            Delegation.ReceptionDate.desc()
        ).limit(20).all()

        return [
            {
              "id": d.Delegation.id,
              "code": d.Delegation.Code,
              "name": d.Delegation.Name,
              "status": d.Delegation.Status,
              "request_date": d.Delegation.RequestDate,
              "reception_date": d.Delegation.ReceptionDate,
              "total_money": float(d.Delegation.TotalMoney) if d.Delegation.TotalMoney is not None else None,
              "content": d.Delegation.Content,
              "location": d.Delegation.Location,
              "totalPerson": float(d.Delegation.TotalPerson) if d.Delegation.TotalPerson is not None else None,
              "phoneNumber": d.Delegation.PhoneNumber,
              "phongBan": d.PhongBanName,
              "staffReceptionName": d.StaffFullName
            }
            for d in results
        ]

    finally:
        db.close()
