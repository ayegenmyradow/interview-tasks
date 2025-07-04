import { useRef } from "react";
import _fetch from "../services/fetch";
import { toast } from "react-toastify";

/* eslint-disable @typescript-eslint/no-explicit-any */
function PositionEditModal({ row, onClose, onSuccess }: any) {
  const formRef = useRef<HTMLFormElement>(null);
  const submit = (evt: any) => {
    evt.preventDefault();
    const form: any = formRef.current;
    const body = {
      id: row.id,
      name: form.name.value,
      baseSalary: form.baseSalary.value,
      department: form.department.value,
      description: form.description.value,
    };
    _fetch(`Positions/${row.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "Application/JSON",
      },
      body,
    })
      .then(onSuccess)
      .catch((err) => {
        toast.error(err.message);
      });
  };

  return (
    <div
      className="modal fade show d-block"
      style={{ backgroundColor: "rgba(0, 0, 0, 0.5)" }}
    >
      <div className="modal-dialog modal-lg modal-dialog-centered">
        <div className="modal-content rounded-4 shadow-lg">
          <div className="modal-header">
            <h5 className="modal-title">Edit Position #{row.id}</h5>
            <button
              type="button"
              className="btn-close "
              onClick={onClose}
            ></button>
          </div>
          <form className="modal-body" onSubmit={submit} ref={formRef}>
            <div className="mb-3">
              <label htmlFor="name_id" className="form-label">
                Name
              </label>
              <input
                type="text"
                className="form-control"
                id="name_id"
                name="name"
                defaultValue={row.name}
                required
              />
            </div>

            <div className="mb-3">
              <label htmlFor="baseSalary_id" className="form-label">
                Base Salary
              </label>
              <input
                type="text"
                className="form-control"
                id="baseSalary_id"
                name="baseSalary"
                defaultValue={row.baseSalary}
                required
              />
            </div>

            <div className="mb-3">
              <label htmlFor="department_id" className="form-label">
                Department
              </label>
              <input
                type="text"
                className="form-control"
                id="department_id"
                name="department"
                defaultValue={row.department}
                required
              />
            </div>

            <div className="mb-3">
              <label htmlFor="description_id" className="form-label">
                Description
              </label>
              <input
                type="text"
                className="form-control"
                id="description_id"
                name="description"
                defaultValue={row.description}
                required
              />
            </div>

            <div className="modal-footer d-flex justify-content-between">
              <button className="btn btn-outline-danger" onClick={onClose}>
                Close
              </button>
              <button type="submit" className="btn btn-outline-primary">
                Edit
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}

export default PositionEditModal;
