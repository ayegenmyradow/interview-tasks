/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable react-refresh/only-export-components */
import { lazy, Suspense } from "react";
import Loader from "../components/Loader";

export const withLazyComponent = (LazyComponent: any, SuspenseComponent: any) =>
  function __c(props: any) {
    return (
      <Suspense fallback={SuspenseComponent}>
        <LazyComponent {...props} />
      </Suspense>
    );
  };

export const EmployeeList = withLazyComponent(
  lazy(() => import("./employee/list")),
  <Loader />
);

export const PositionsList = withLazyComponent(
  lazy(() => import("./positions/list")),
  <Loader />
);


export const AboutCompany = withLazyComponent(
  lazy(() => import("./static-pages/about-company")),
  <Loader />
);
