import React, { FC, useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import { toast } from "react-toastify";
import axios from "axios";
import { Car } from "../../interfaces/car";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSpinner } from "@fortawesome/free-solid-svg-icons";

interface Props {
  car: Car;
  showOnlyActive: boolean;
  search: string;
  setCars: any;
  close: () => void;
}

export const ToggleCarActivationModal: FC<Props> = ({
  car,
  showOnlyActive,
  search,
  setCars,
  close,
}) => {
  const [loading, setLoading] = useState(false);

  const toggleCarActivation = async (): Promise<void> => {
    setLoading(true);
    try {
      const response = await axios.put(
        `${process.env.REACT_APP_BASE_URL}/cars/active/${car.licensePlateNumber}`
      );
      const { error, success } = response.data;
      if (success) {
        setCars((prevCars: Car[]) =>
          prevCars
            .map((prevCar: Car) =>
              prevCar.licensePlateNumber === car.licensePlateNumber
                ? { ...prevCar, active: !prevCar.active }
                : prevCar
            )
            .filter((prevCar: Car) => (showOnlyActive ? prevCar.active : true))
            .filter(
              (prevCar: Car) =>
                prevCar.licensePlateNumber
                  .toLowerCase()
                  .includes(search.toLowerCase()) ||
                prevCar.year.toString().includes(search) ||
                prevCar.manufacturer
                  .toLowerCase()
                  .includes(search.toLowerCase()) ||
                prevCar.color.toLowerCase().includes(search.toLowerCase())
            )
        );
        toast(car.active ? "הרכב הוסר!" : "הרכב הופעל!");
        close();
      } else {
        console.log(error);
      }
      setLoading(false);
    } catch (err) {
      setLoading(false);
      console.log(err);
    }
  };

  return (
    <Modal
      show
      aria-labelledby="contained-modal-title-vcenter"
      centered
      style={{ minHeight: "200px" }}
    >
      <Modal.Dialog style={{ width: "100%", margin: "0" }} dir="rtl">
        <Modal.Header closeButton onClick={close}>
          <Modal.Title>{car.active ? "הסרת רכב" : "הפעלת רכב"}</Modal.Title>
        </Modal.Header>

        <Modal.Body dir="rtl">
          <p>
            האם את/ה בטוח/ה שברצונך {car.active ? "להסיר" : "להפעיל"} את הרכב `
            <span
              style={{ fontStyle: "italic" }}
            >{`${car.model}, ${car.manufacturer}`}</span>
            `?
          </p>
        </Modal.Body>

        <Modal.Footer style={{ margin: "auto" }}>
          <Button
            variant="primary"
            onClick={toggleCarActivation}
            style={{ width: "100px" }}
          >
            {car.active ? "מחיקה" : "הפעלה"}
            {loading && (
              <FontAwesomeIcon
                icon={faSpinner}
                spin={true}
                style={{ marginRight: "10px" }}
              />
            )}
          </Button>
          <Button
            variant="secondary"
            onClick={close}
            style={{ width: "100px" }}
          >
            ביטול
          </Button>
        </Modal.Footer>
      </Modal.Dialog>
    </Modal>
  );
};
