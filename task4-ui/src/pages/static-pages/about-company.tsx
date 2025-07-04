import React from "react";

const companyInfo = {
  name: "COMPANY Ltd.",
  overview:
    "COMPANY is a forward-thinking software development company specializing in custom enterprise solutions, cloud integrations, and mobile apps. We empower businesses through technology.",
  mission:
    "To deliver innovative and scalable technology solutions that drive success.",
  vision:
    "To be the leading catalyst for digital transformation across industries.",
};

const teamMembers = [
  {
    id: 1,
    name: "John Doe",
    position: "CEO",
    image: "https://placehold.jp/150x150.png",
  },
  {
    id: 2,
    name: "Jane Smith",
    position: "CTO",
    image: "https://placehold.jp/150x150.png",
  },
  {
    id: 3,
    name: "Mark Lee",
    position: "Lead Developer",
    image: "https://placehold.jp/150x150.png",
  },
  {
    id: 4,
    name: "Emily Clark",
    position: "Product Manager",
    image: "https://placehold.jp/150x150.png",
  },
];

const AboutCompany = () => {
  return (
    <div className="container my-5">
      <h1 className="text-center mb-5">{companyInfo.name}</h1>

      <div className="mb-5">
        <h4>Overview</h4>
        <p>{companyInfo.overview}</p>
        <h5 className="mt-4">Mission</h5>
        <p>{companyInfo.mission}</p>
        <h5 className="mt-4">Vision</h5>
        <p>{companyInfo.vision}</p>
      </div>

      <div>
        <h4 className="mb-4">Meet Our Team</h4>
        <div className="row">
          {teamMembers.map((member) => (
            <div className="col-md-3 col-sm-6 mb-4" key={member.id}>
              <div className="card h-100 text-center border-0 shadow-sm">
                <img
                  src={member.image}
                  alt={member.name}
                  className="card-img-top rounded-circle p-3"
                  style={{ width: "100%", height: "auto", objectFit: "cover" }}
                />
                <div className="card-body">
                  <h5 className="card-title mb-1">{member.name}</h5>
                  <p className="card-text text-muted">{member.position}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AboutCompany;
