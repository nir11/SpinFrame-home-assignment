import axios from "axios";
import React, { useEffect, useState } from "react";
import Table from "react-bootstrap/Table";
import Button from "react-bootstrap/Button";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faEdit,
  faToggleOn,
  faToggleOff,
} from "@fortawesome/free-solid-svg-icons";
import { CarFormModal } from "../car-form-modal/car-form-modal";
import { Car } from "../../interfaces/car";
import { ToggleCarActivationModal } from "../toggle-car-activation-modal/toggle-car-activation-modal";
import InputGroup from "react-bootstrap/InputGroup";
import { debounce } from "lodash";
import { Tooltip } from "react-tooltip";
import "./cars-table.scss";

export const CarsTable = () => {
  const [loading, setLoading] = useState(true);
  const [cars, setCars] = useState<Car[]>([]);
  const [showOnlyActive, setShowOnlyActive] = useState(true);
  const [search, setSearch] = useState("");
  const [carFormModal, setCarFormModal] = useState<{
    open: boolean;
    car: Car | null;
  }>({
    open: false,
    car: null,
  });
  const [toggleCarActivationModal, setToggleCarActivationModal] = useState<{
    open: boolean;
    car: Car | null;
  }>({
    open: false,
    car: null,
  });
  const [searchCars] = useState(() => {
    return debounce(async (search: string): Promise<void> => {
      await getCars(search);
    }, 500);
  });

  useEffect(() => {
    getCars(search);
  }, [showOnlyActive]);

  const getCars = async (searchStr: string) => {
    try {
      setLoading(true);
      const response = await axios.get(
        `${process.env.REACT_APP_BASE_URL}/cars?onlyActive=${showOnlyActive}&search=${searchStr}`
      );
      const { error, payload, success } = response.data;
      if (success) {
        setCars(payload);
      } else {
        console.log(error);
      }
      setLoading(false);
    } catch (err) {
      console.log(err);
      setLoading(false);
    }
  };

  return (
    <div className="cars-table-container">
      <Tooltip id="tooltip" />

      {carFormModal.open && (
        <CarFormModal
          initialCar={carFormModal.car}
          setCars={setCars}
          close={() =>
            setCarFormModal({
              open: false,
              car: null,
            })
          }
        />
      )}

      {toggleCarActivationModal.open && (
        <ToggleCarActivationModal
          car={toggleCarActivationModal.car as Car}
          showOnlyActive={showOnlyActive}
          search={search}
          setCars={setCars}
          close={() =>
            setToggleCarActivationModal({
              open: false,
              car: null,
            })
          }
        />
      )}

      <div className="cars-header">
        <div className="table-actions-container">
          <div>
            <InputGroup
              className="d-flex align-items-center"
              style={{ marginBottom: "5px" }}
            >
              <InputGroup.Checkbox
                id="checkbox"
                aria-label="Checkbox for following text input"
                checked={showOnlyActive}
                onChange={(e: any) => setShowOnlyActive(e.target.checked)}
              />
              <label htmlFor="checkbox" className="show-only-active">
                Show only active
              </label>
            </InputGroup>
            <input
              type="search"
              className="form-control"
              placeholder="Search by car license number, color, manufacturer, model, year..."
              style={{ width: "400px" }}
              value={search}
              onChange={(e: any) => {
                setSearch(e.target.value);
                setLoading(true);
                searchCars(e.target.value);
              }}
            />
          </div>
        </div>
        <Button
          variant="primary"
          onClick={() => {
            setCarFormModal({
              open: true,
              car: null,
            });
          }}
        >
          Add new Car
        </Button>
      </div>

      <Table striped bordered hover className="mt-4">
        <thead>
          <tr>
            <th></th>
            <th>License Plate Number</th>
            <th>Color</th>
            <th>Manufacturer</th>
            <th>Model</th>
            <th>Year</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {loading ? (
            <tr>
              <td></td>
              <td colSpan={5}>Loading...</td>
            </tr>
          ) : (
            <>
              {cars.length == 0 ? (
                <tr>
                  <td></td>
                  <td colSpan={5}>No cars found</td>
                </tr>
              ) : (
                cars.map((car: Car, i: number) => {
                  return (
                    <tr>
                      <td>{i + 1}</td>
                      <td
                        style={{
                          textDecoration: !car.active ? "line-through" : "none",
                        }}
                      >
                        {car.licensePlateNumber}
                      </td>
                      <td
                        style={{
                          textDecoration: !car.active ? "line-through" : "none",
                        }}
                      >
                        {car.color}
                      </td>
                      <td
                        style={{
                          textDecoration: !car.active ? "line-through" : "none",
                        }}
                      >
                        {car.manufacturer}
                      </td>
                      <td
                        style={{
                          textDecoration: !car.active ? "line-through" : "none",
                        }}
                      >
                        {car.model}
                      </td>
                      <td
                        style={{
                          textDecoration: !car.active ? "line-through" : "none",
                        }}
                      >
                        {car.year}
                      </td>
                      <td>
                        <button
                          key={car.licensePlateNumber}
                          className="car-button"
                          data-tooltip-id="tooltip"
                          data-tooltip-content="Update"
                        >
                          <FontAwesomeIcon
                            icon={faEdit}
                            color="Mediumslateblue"
                            onClick={() => {
                              setCarFormModal({
                                open: true,
                                car,
                              });
                            }}
                          />
                        </button>
                        <button
                          key={car.licensePlateNumber + i}
                          className="car-button"
                          data-tooltip-id="tooltip"
                          data-tooltip-content={
                            car.active ? "Remove" : "Activate"
                          }
                        >
                          <FontAwesomeIcon
                            icon={car.active ? faToggleOn : faToggleOff}
                            color="Mediumslateblue"
                            onClick={() => {
                              setToggleCarActivationModal({
                                open: true,
                                car,
                              });
                            }}
                          />
                        </button>
                      </td>
                    </tr>
                  );
                })
              )}
            </>
          )}
        </tbody>
      </Table>
    </div>
  );
};
