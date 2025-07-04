import { Link, useLocation } from "react-router-dom";

function Navbar() {
  const { pathname } = useLocation();

  const isAboutCompanyActive = pathname.startsWith("/about-company");
  const isPositionsActive = pathname.startsWith("/positions");
  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary">
      <div className="container">
        <a className="navbar-brand" href="#">
          Employee Management
        </a>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <Link
                className={`nav-link ${
                  isAboutCompanyActive || isPositionsActive ? "" : "active"
                }`}
                to="/"
              >
                Employee List
              </Link>
            </li>
            <li className="nav-item">
              <Link
                className={`nav-link ${isAboutCompanyActive ? "active" : ""}`}
                to="/about-company"
              >
                About Company
              </Link>
            </li>

            <li className="nav-item">
              <Link
                className={`nav-link ${isPositionsActive ? "active" : ""}`}
                to="/positions"
              >
                Positions
              </Link>
            </li>
          </ul>
          <form className="d-flex" role="search">
            <input
              className="form-control me-2"
              type="search"
              placeholder="Search"
              aria-label="Search"
            />
            <button className="btn btn-outline-primary" type="submit">
              Search
            </button>
          </form>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
