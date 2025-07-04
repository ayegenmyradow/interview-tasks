/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useRef, useState } from "react";
import _fetch from "../services/fetch";
import { toast } from "react-toastify";
import { getAllPositions } from "../services/positionsApi";

function EmployeeCreateModal({ onSuccess }: any) {
  const [show, setShow] = useState(false);
  const [positions, setPositions] = useState([]);
  const handleOpen = () => setShow(true);
  const handleClose = () => setShow(false);
  const formRef = useRef<HTMLFormElement>(null);
  const submit = (evt: any) => {
    evt.preventDefault();

    const form: any = formRef.current;
    const body = {
      firstName: form.firstName.value,
      lastName: form.lastName.value,
      email: form.email.value,
      dateOfBirth: form.dateOfBirth.value,
      salary: form.salary.value,
      positionId: form.positionId.value,
    };

    _fetch(`Employees`, {
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

  useEffect(() => {
    getAllPositions({ pageSize: 10000, pageNumber: 1 }).then((res) => {
      setPositions(res.positions);
    });
  }, []);

  return (
    <>
      {/* Trigger Button */}
      <button className="btn btn-outline-success" onClick={handleOpen}>
        Create Employee
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
                <h5 className="modal-title">Create Employee</h5>
                <button
                  type="button"
                  className="btn-close "
                  onClick={handleClose}
                ></button>
              </div>
              <form className="modal-body" onSubmit={submit} ref={formRef}>
                <div className="mb-3">
                  <label htmlFor="firstname" className="form-label">
                    First Name
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="firstname"
                    name="firstName"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="lastname" className="form-label">
                    Last Name
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    id="lastname"
                    name="lastName"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="email" className="form-label">
                    Email
                  </label>
                  <input
                    type="email"
                    className="form-control"
                    id="email"
                    name="email"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="dob" className="form-label">
                    Date of Birth
                  </label>
                  <input
                    type="date"
                    className="form-control"
                    id="dob"
                    name="dateOfBirth"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label htmlFor="salary" className="form-label">
                    Salary
                  </label>
                  <input
                    type="number"
                    className="form-control"
                    id="salary"
                    name="salary"
                    required
                  />
                </div>

                <div className="mb-4">
                  <label htmlFor="positioned" className="form-label">
                    Position
                  </label>
                  <select
                    className="form-select"
                    id="positioned"
                    name="positionId"
                    required
                  >
                    <option value="" disabled defaultChecked>
                      Select Position
                    </option>
                    {positions.map((p:any) => (
                      <option key={`position_id_${p.id}`} value={p.id}>
                        {p.name}
                      </option>
                    ))}
                  </select>
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

export default EmployeeCreateModal;
