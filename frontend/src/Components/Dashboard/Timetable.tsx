import React from "react";
import { Button, Stack } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import getIsLoggedInReduxProxy from "../../Redux/ReduxProxy/getIsLoggedInReduxProxy";
import CalendarCollapsible from "../Shared/CalendarCollapsible";

const mapStateToProps = (state: any) => ({});

const mapDispatchToProps = (dispatch: any) => ({});

type Props = {}

function Timetable(props: Props) {
  const { t } = useTranslation('events');
  return <>
    <Stack>
      <div className="d-flex justify-content-end m-1 p-1">
        <Button>{t('addEvent')}</Button>
      </div>
      <CalendarCollapsible />
    </Stack>
  </>;
}

export default Timetable;
