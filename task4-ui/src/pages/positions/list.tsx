/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { DeleteIcon, EditIcon } from "../../components/icons";
import _fetch from "../../services/fetch";
import { getAllPositions } from "../../services/positionsApi";
import PositionCreateModal from "../../components/position-create-modal";
import PositionEditModal from "../../components/position-edit-modal";

const ROWS_PER_PAGE = 100;

const Positions = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [edit, setEdit] = useState<any>();

  const [data, setData] = useState<any>({
    positions: [],
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
  }, []);

  function retrieve() {
    getAllPositions({ pageNumber: 1, pageSize: ROWS_PER_PAGE })
      .then((res: any) => {
        setData(res);
      })
      .catch((err) => {
        toast.error(err.message);
      });
  }

  console.log(data);

  return (
    <div className="container mt-4">
      <div className="my-3 d-flex justify-content-between mt-4 align-items-center">
        <h2>Positions</h2>
        <PositionCreateModal onSuccess={retrieve} />
      </div>
      <table className="table table-bordered table-striped table-hover">
        <thead className="table-dark">
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Base Salary</th>
            <th>Department</th>
            <th>Description</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data.positions.map((row: any) => (
            <tr key={row.id}>
              <td>{row.id}</td>
              <td>{row.name}</td>
              <td>{row.baseSalary}</td>
              <td>{row.department}</td>
              <td>{row.description}</td>
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
                        _fetch(`Positions/${row.id}`, {
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
        <PositionEditModal
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

export default Positions;
