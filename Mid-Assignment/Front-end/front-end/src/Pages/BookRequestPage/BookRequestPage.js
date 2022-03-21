import axios from "axios";
import React, { useEffect, useState } from "react";
import { Accordion } from "react-bootstrap";

const BookRequestPage = () => {
    const [requestList, setRequestList] = useState([]);
    const [requestDetails, setRequestDetails] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');

    const userId = 3;

    //GetRequests api
    useEffect(() => {
        let isCancel = false;
        axios({
            method: 'GET',
            url: `https://localhost:7279/api/book-request/${userId}`
        }).then(response => {
            if (!isCancel) {
                setRequestList(response.data.requests);
                setRequestDetails(response.data.details);
                setIsLoading(false);
            }
        }).catch((error) => {
            if (!isCancel) {
                setIsLoading(false);
                setError('Something wrong');
            }
        });
        return () => {
            isCancel = true;
        }
    }, []);

    //Request status
    if (isLoading) return (
        <div>Loading</div>
    )
    if (error) return (
        <div style={{ color: 'red' }}>{error}</div>
    )


    return (
        <div>
            <h1 style={{ textAlign: 'center' }}>BookRequestPage</h1>

            <div style={{ marginTop: '80px' }}>
                {requestList.map(request => (
                    <Accordion key={request.requestId}>
                        <Accordion.Item eventKey="0">
                            <Accordion.Header>Date Requested: {request.dateOfRequest}</Accordion.Header>
                            <Accordion.Body>
                                {(request.status === 0) && 'Waiting'}
                                {(request.status === 1) && 'Rejected'}
                                {(request.status === 2) && 'Approved'}
                                {requestDetails.filter(detail => detail.requestId === request.requestId).map(detail => (
                                    <div key={detail.bookName}>{detail.bookName}</div>
                                ))}
                            </Accordion.Body>
                        </Accordion.Item>
                    </Accordion>
                ))}
            </div>
        </div >
    )
}
export default BookRequestPage;
