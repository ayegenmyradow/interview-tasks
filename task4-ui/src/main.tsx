import { Suspense } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import Navbar from "./components/Navbar";

import { AboutCompany, EmployeeList, PositionsList } from "./pages";

import "react-toastify/dist/ReactToastify.css";
import "./assets/css/bootstrap.min.css";

import "./index.css";

createRoot(document.getElementById("root")!).render(
  <Router>
    <Navbar />
    <Suspense fallback={<div> Something went wrong </div>}>
      <Routes>
        <Route path="/" element={<EmployeeList />} />
        <Route path="/positions" element={<PositionsList />} />
        <Route path="/about-company" element={<AboutCompany />} />
        <Route path="*" element={<div>Not Found Page</div>} />
      </Routes>
    </Suspense>
    <ToastContainer />
  </Router>
);
