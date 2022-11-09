import { Button, Checkbox, Input } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Row, Col, Modal } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import PersonResponse from "../../ApiClient/Schools/Definitions/Responses/PersonResponse";
import InfiniteScrollWrapper from "./InfiniteScrollWrapper";

const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
});

const mapDispatchToProps = (dispatch: any) => ({});

type Props = {
  onHide: () => void;
  onConfirm: (peopleGuids: string[]) => void;
  getPeople: (schoolGuid: string, schoolRole: string, query: string, page: number) => Promise<PersonResponse[]>;
  show: boolean;
  currentSchoolGuid?: string;
  discriminator?: string;
  selectedPeople?: string[];
};

function PeoplePicker(props: Props) {
  const { t } = useTranslation("peoplePicker");
  const [selectedPeople, setSelectedPeople] = useState<string[]>([]);
  const [query, setQuery] = useState("");
  useEffect(() => {
    setSelectedPeople(props.selectedPeople ?? []);
  }, [props.show]);
  return (
    <Modal show={props.show} onHide={props.onHide}>
      <Modal.Header closeButton>{t("peoplePicker")}</Modal.Header>
      <Modal.Body>
        <Input type="text" className="w-100" placeholder={t("searchPeople")} value={query} onChange={(e) => setQuery(e.target.value)} />
        <div className="vh-50 overflow-scroll">
          <InfiniteScrollWrapper
            effect={[query]}
            mapper={(person: PersonResponse, index) => (
              <div
                key={index}
                className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
                onClick={() => {
                  selectedPeople.includes(person.guid)
                    ? setSelectedPeople((s) => s.filter((p) => p != person.guid))
                    : setSelectedPeople((s) => [...s, person.guid]);
                }}
              >
                <div className="my-auto">{`${person.name} ${person.surname}`}</div>
                <div>
                  <Checkbox
                    checked={selectedPeople.includes(person.guid)}
                    onChange={(e, o) =>
                      o ? setSelectedPeople((s) => [...s, person.guid]) : setSelectedPeople((s) => s.filter((p) => p != person.guid))
                    }
                  />
                </div>
              </div>
            )}
            fetch={async (page: number) => {
              if (!props.currentSchoolGuid) return [];
              return (await props.getPeople(props.currentSchoolGuid!, props.discriminator ?? "", query, page)) as [];
            }}
          />
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button
          variant="outlined"
          onClick={() => {
            props.onConfirm([...new Set(selectedPeople)]);
            props.onHide();
          }}
        >
          {t("confirm")}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default connect(mapStateToProps, mapDispatchToProps)(PeoplePicker);
