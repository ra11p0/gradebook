import { faWindowMaximize } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Button, TableCell, TableRow } from "@mui/material";
import Tippy from "@tippyjs/react";
import moment from "moment";
import React from "react";
import { Col, Row, Table } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";
import EducationCycleResponse from "../../../../../ApiClient/Schools/Definitions/Responses/EducationCycleResponse";
import SchoolsProxy from "../../../../../ApiClient/Schools/SchoolsProxy";
import InfiniteScrollWrapper from "../../../../Shared/InfiniteScrollWrapper";
import Person from "../../../../Shared/Person";
import Header from "./Header";

type Props = {};

function CyclesList({ }: Props) {
  const { t } = useTranslation('educationCycle');
  return (
    <Row>
      <Col>
        <Row>
          <Col>
            <Header />
          </Col>
        </Row>
        <Row>
          <Col>
            <InfiniteScrollWrapper
              wrapper={(items) => (<>
                <Table striped bordered hover responsive>
                  <thead>
                    <tr>
                      <th>
                        {t('educationCycleName')}
                      </th>
                      <th>
                        {t('createdDate')}
                      </th>
                      <th>
                        {t('creator')}
                      </th>
                      <th>
                        {t('actions')}
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {items}
                  </tbody>
                </Table>
              </>)}
              mapper={(item, index) => (
                <tr key={index}>
                  <td>
                    {item.name}
                  </td>
                  <td>
                    {moment.utc(item.createdDate).local().format('ll')}
                  </td>
                  <td>
                    <Person {...item.creator} />
                  </td>
                  <td>
                    <Actions {...item} />
                  </td>
                </tr>
              )}
              fetch={async (page: number) => {
                return (await SchoolsProxy.educationCycles.getEducationCyclesInSchool(page)).data
              }}
            />
          </Col>
        </Row>
      </Col>
    </Row>
  );
}

function Actions(element: EducationCycleResponse) {
  const { t } = useTranslation("educationCycle");
  return (
    <div className="d-flex gap-1 flex-wrap">
      <Link to={`/educationCycle/show/${element.guid}`}>
        <Tippy content={t("showEducationCycle")} arrow={true} animation={"scale"}>
          <Button variant="outlined" className="showProfileButton">
            <FontAwesomeIcon icon={faWindowMaximize} />
          </Button>
        </Tippy>
      </Link>
    </div>
  );
}

export default CyclesList;
