import { CarsTable } from "./components/cars-table/cars-table";
import { ToastContainer, toast } from "react-toastify";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCar } from "@fortawesome/free-solid-svg-icons";
import { faGithubSquare, faLinkedin } from "@fortawesome/free-brands-svg-icons";
import "react-toastify/dist/ReactToastify.css";
import "react-tooltip/dist/react-tooltip.css";
import "./App.scss";

function App() {
  return (
    <div className="App">
      <ToastContainer
        position={toast.POSITION.BOTTOM_CENTER}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={true}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        enableMultiContainer
        limit={1}
        autoClose={500} // close after 500 ms
      />

      <header>
        <div>
          <h1>ניהול צי רכב</h1>
          <FontAwesomeIcon icon={faCar} size="2x" />
        </div>

        <div className="developer-details">
          <span>Created by Nir Almog</span>
          <FontAwesomeIcon
            icon={faGithubSquare}
            onClick={() => window.open("https://github.com/nir11", "_blank")}
            size="2x"
            className="github-icon"
          />
          <FontAwesomeIcon
            icon={faLinkedin}
            onClick={() =>
              window.open(
                "https://www.linkedin.com/in/nir-almog-9a4202151/",
                "_blank"
              )
            }
            size="2x"
            className="linkedin-icon"
          />
        </div>
      </header>
      <CarsTable />
    </div>
  );
}

export default App;
