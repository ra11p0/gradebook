import { connect } from 'react-redux';
import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import DashboardIndex from '../Components/Dashboard/DashboardIndex';
import DashboardNavigation from '../Components/Dashboard/DashboardNavigation';
import Absence from '../Components/Dashboard/Absence';
import Grades from '../Components/Dashboard/Grades';
import Subject from '../Components/Dashboard/Subject';
import Timetable from '../Components/Dashboard/Timetable';

const mapStateToProps = (state: any) =>({
    isLoggedIn: state.common.isLoggedIn
});

const mapDispatchToProps = (dispatch: any) => ({

});

interface DashboardProps{
    isLoggedIn?: boolean
}

class Dashboard extends React.Component<DashboardProps>{
    render(){
        return (
        <div className='m-3 card'>
            <div className='card-header'>
                <DashboardNavigation/>
            </div>
            <div className='card-body'>
                 <Routes>
                    <Route path="*" element={<DashboardIndex/>}></Route>
                    <Route path="absence" element={<Absence/>}></Route>
                    <Route path="grades" element={<Grades/>}></Route>
                    <Route path="subject" element={<Subject/>}></Route>
                    <Route path="timetable" element={<Timetable/>}></Route>
                 </Routes>
            </div>
            {
                !this.props.isLoggedIn &&
                <Navigate to='/'/>
            }
        </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Dashboard);
