import Grid from "@mui/material/Unstable_Grid2";
import Word from "./Word";

export default function RelatedWords({ data }) {
return (
<div>
  { data.map((wr) => (
  <Grid container spacing={2}>
    <Grid>
      <Word word={wr.word}></Word>
    </Grid>
    <Grid>
      <Word word={wr.primaryTranslation.word}></Word>
    </Grid>
  </Grid>
  ))}
</div>
);
}