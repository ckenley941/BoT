import React from "react";

import Accordion from "@mui/material/Accordion";
import AccordionSummary from "@mui/material/AccordionSummary";
import AccordionDetails from "@mui/material/AccordionDetails";

import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Grid from "@mui/material/Grid";

import ExpandMoreIcon from "@mui/icons-material/ExpandMore";

import Word from "./Word";
import WordExample from "./WordExample";

export default function WordContext({ data }) {
return (
<Card>
  <h4>{data.contextDesc}</h4>
  <CardContent>
    {data.words.map((w, i) => (
    <Accordion>
      <AccordionSummary expandIcon={<ExpandMoreIcon />}
      aria-controls="panel1a-content"
      id="panel1a-header">
      <Grid container spacing={2}>
        <Word word={w.word}></Word>
      </Grid>
      </AccordionSummary>
      <AccordionDetails>
        <WordExample translation1={w.examples[0].translation1} translation2={w.examples[0].translation2}></WordExample>
      </AccordionDetails>
    </Accordion>
    ))}
  </CardContent>
</Card>
);
}