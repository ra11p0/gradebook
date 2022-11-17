import moment from "moment";
import React from "react";
import { Button, Stack } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import getIsLoggedInReduxProxy from "../../Redux/ReduxProxy/getIsLoggedInReduxProxy";
import CalendarCollapsible from "../Shared/CalendarCollapsible/CalendarCollapsible";

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
      <CalendarCollapsible events={[{
        startDate: moment('2022-11-18 10:10'),
        endDate: moment('2022-11-18 12:05'),
        title: 'Projektowanie portali korporacyjnych; SPEC.: Inżynieria technologii internetowych ',
      }, {
        startDate: moment('2022-11-20 12:30'),
        endDate: moment('2022-11-20 14:25'),
        title: 'PDW VII: Sztuczna Inteligencja ',
      }, {
        startDate: moment('2022-11-19 16:50'),
        endDate: moment('2022-11-19 18:45'),
        title: 'PDW VI: Zarządzanie projektami IT',
      }, {
        startDate: moment('2022-11-18 19:00'),
        endDate: moment('2022-11-18 20:55'),
        title: 'PDW VII: Sztuczna Inteligencja',
      }, {
        startDate: moment('2022-11-17 11:20'),
        endDate: moment('2022-11-17 13:20'),
        title: 'Zaawansowane technologie internetowe; SPEC.: Inżynieria technologii internetowych ',
      }, {
        startDate: moment('2022-11-17 15:20'),
        endDate: moment('2022-11-17 16:20'),
        title: 'PDW VIII: Wprowadzenie do prawa i praw autorskich',
      }, {
        startDate: moment('2022-11-16 16:20'),
        endDate: moment('2022-11-16 19:20'),
        title: 'Projektowanie wielowarstwowych aplikacji internetowych; SPEC.: Inżynieria technologii internetowych',
      }, {
        startDate: moment('2022-11-16 15:20'),
        endDate: moment('2022-11-16 17:20'),
        title: 'dsadsa',
      },]} />
    </Stack>
  </>;
}

export default Timetable;
