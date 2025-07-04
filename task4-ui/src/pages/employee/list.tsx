/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from "react";
import EmployeeCreateModal from "../../components/employee-create-modal";
import { getAllEmployees } from "../../services/employeeApi";
import { toast } from "react-toastify";
import { dateFormat } from "../../services/dateFormat";
import { DeleteIcon, EditIcon } from "../../components/icons";
import EmployeeEditModal from "../../components/employee-edit-modal";
import _fetch from "../../services/fetch";

const ROWS_PER_PAGE = 20;

const EmployeeList = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [edit, setEdit] = useState<any>();

  const [data, setData] = useState<any>({
    employees: [],
    hasNextPage: false,
    hasPreviousPage: false,
    totalCount: 0,
    totalPages: 0,
  });

  const handlePageChange = (page: any) => {
    setCurrentPage(page);
  };

  useEffect(() => {
    retrieve();
  }, [currentPage]);

  function retrieve() {
    getAllEmployees({ pageNumber: 1, pageSize: ROWS_PER_PAGE })
      .then((res: any) => {
        setData(res);
      })
      .catch((err) => {
        toast.error(err.message);
      });
  }

  return (
    <div className="container mt-4">
      <div className="my-3 d-flex justify-content-between mt-4 align-items-center">
        <h2>Employee List</h2>
        <EmployeeCreateModal onSuccess={retrieve} />
      </div>
      <table className="table table-bordered table-striped table-hover">
        <thead className="table-dark">
          <tr>
            <th>#</th>
            <th>Full Name</th>
            <th>Email</th>
            <th>Job</th>
            <th>Salary</th>
            <th>Date of birth</th>
            <th>Hire date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.employees.map((row: any) => (
            <tr key={row.id}>
              <td>{row.id}</td>
              <td>{row.fullName}</td>
              <td>{row.email}</td>
              <td>
                {row.department} - {row.positionName}
              </td>
              <td>{row.salary}</td>
              <td>{dateFormat(new Date(row.dateOfBirth), "%Y-%m-%d", true)}</td>
              <td>{dateFormat(new Date(row.hireDate), "%Y-%m-%d", true)}</td>
              <td>
                <div>
                  <button
                    className="btn btn-primary p-2"
                    onClick={() => setEdit(row)}
                  >
                    <EditIcon />
                  </button>
                  <button
                    className="btn btn-danger p-2 ms-1"
                    onClick={() => {
                      if (window.confirm("Are you sure?")) {
                        _fetch(`Employees/${row.id}`, {
                          method: "DELETE",
                          headers: {
                            "Content-Type": "Application/JSON",
                          },
                          body: {},
                        })
                          .then(() => {
                            retrieve();
                          })
                          .catch(() => {
                            // toast.error(err.message);
                            retrieve();
                          });
                      }
                    }}
                  >
                    <DeleteIcon />
                  </button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Pagination */}
      <nav>
        <ul className="pagination justify-content-center">
          {Array.from({ length: data.totalPages }, (_, i) => (
            <li
              key={i}
              className={`page-item ms-2 ${
                currentPage === i + 1 ? "active" : ""
              }`}
            >
              <button
                className="page-link"
                onClick={() => handlePageChange(i + 1)}
              >
                {i + 1}
              </button>
            </li>
          ))}
        </ul>
      </nav>
      {edit ? (
        <EmployeeEditModal
          row={edit}
          onClose={() => setEdit(null)}
          onSuccess={() => {
            retrieve();
            setEdit(null);
          }}
        />
      ) : null}
    </div>
  );
};

export default EmployeeList;
