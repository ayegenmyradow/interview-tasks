/* eslint-disable @typescript-eslint/no-explicit-any */
import { useRef, useState } from "react";
import _fetch from "../services/fetch";
import { toast } from "react-toastify";

function PositionCreateModal({ onSuccess }: any) {
  const [show, setShow] = useState(false);

  const handleOpen = () => setShow(true);
  const handleClose = () => setShow(false);
  const formRef = useRef<HTMLFormElement>(null);
  const submit = (evt: any) => {
    evt.preventDefault();

    const form: any = formRef.current;
    const body = {
      name: form.name.value,
      baseSalary: form.baseSalary.value,
      department: form.department.value,
      description: form.description.value,
    };

    _fetch(`Positions`, {
      method: "POST",
      headers: {
        "Content-Type": "Application/JSON",
      },
      body,
    })
      .then(() => {
        onSuccess();
        setShow(false);
      })
      .catch((err) => {
        toast.error(err.message);
      });
  };
  return (
    <>
      {/* Trigger Button */}
      <button className="btn btn-outline-success" onClick={handleOpen}>
        Create Position
      </button>

      {/* Modal */}
      {show && (
        <div
          className="modal fade show d-block"
          style={{ backgroundColor: "rgba(0, 0, 0, 0.5)" }}
        >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content rounded-4 shadow-lg">
              <div className="modal-header">
                <h5 className="modal-title">Create Position</h5>
                <button
                  type="button"
                  className="btn-close "
                  onClick={handleClose}
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
                    required
                  />
                </div>

                <div className="modal-footer d-flex justify-content-between">
                  <button
                    className="btn btn-outline-danger"
                    onClick={handleClose}
                  >
                    Close
                  </button>
                  <button type="submit" className="btn btn-outline-primary">
                    Create
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default PositionCreateModal;
